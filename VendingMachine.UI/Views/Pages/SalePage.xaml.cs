using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VendingMachine.Domain.Coins;
using VendingMachine.Domain.Drinks;
using VendingMachine.UI.Tools;
using VendingMachine.UI.Views.Windows;

namespace VendingMachine.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for SalePage.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        public Int32 ClientMoney { get; set; } = 0;
        public VMCoin[] Coins { get; private set; }
        public VMDrink[] Drinks { get; private set; }



        internal List<BasketDrink> Basket { get; private set; } = new();
        public Int32 OrderPrice => Basket.Select(i => i.TotalPrice).Sum();
        public String OrderPriceString => $"Корзина({OrderPrice} р.)";

        public String ClientMoneyString => $"Вложенная сумма: {ClientMoney} руб.";

        internal BasketDrink O { get; private set; }

        public SalePage()
        {
            InitializeComponent();
            LoadData();
            this.DataContext = this;
            orderListBox.ItemsSource = Basket;
        }

        public async void LoadData()
        {
            App.Base.LoadingRun();

            if (App.VendingMachine is null) await App.Base.LoadVendingMachine();

            Coins = await HttpHelper.GetVmCoins(App.VendingMachine.Id);
            coinsListView.ItemsSource = null;
            coinsListView.ItemsSource = Coins;

            Drinks = await HttpHelper.GetVmDrinks(App.VendingMachine.Id);
            drinksListBox.ItemsSource = null;
            drinksListBox.ItemsSource = Drinks;

            App.Base.LoadingStop();
        }

        private void Money_Click(object sender, RoutedEventArgs e)
        {
            VMCoin coin = (sender as Button).DataContext as VMCoin;
            VMCoin vmCoin = Coins.FirstOrDefault(c => c.Coin.Nominal == coin.Coin.Nominal);
            if (vmCoin is null)
            {
                (sender as Button).IsEnabled = false;
                return;
            }
            vmCoin.AddCount();

            ClientMoney += coin.Coin.Nominal;
            this.DataContext = null;
            this.DataContext = this;
        }

        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizeWindow window = new();
            window.Show();
        }

        private void drinksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            VMDrink vmDrink = listBox.SelectedItem as VMDrink;
            if (vmDrink is null) return;
            if (vmDrink.Count == 0)
            {
                drinksListBox.SelectedIndex = -1;
                App.ShowMessage($"Напиток {vmDrink.Drink.Name} закончился");
                return;
            }

            BasketDrink? basketDrink = Basket.FirstOrDefault(i => i.Drink.Id == vmDrink.Drink.Id);
            if (basketDrink is null) Basket.Add(new BasketDrink(vmDrink.Drink, 1));
            else
            {
                basketDrink.Count++;
            }

            Drinks.FirstOrDefault(d => d.Id == vmDrink.Id).DecreaseCount();

            orderListBox.ItemsSource = null;
            orderListBox.ItemsSource = Basket;
            basketLabel.Content = OrderPriceString;

            drinksListBox.SelectedIndex = -1;
        }

        private void DecreaseItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Guid drinkId = Guid.Parse(button.Tag.ToString());

            BasketDrink basketDrink = Basket.FirstOrDefault(i => i.Drink.Id == drinkId);
            if (basketDrink is null) return;

            if (basketDrink.Count == 1) RemoveItem_Click(sender, e);
            else
            {
                basketDrink.Count--;
                Drinks.FirstOrDefault(d => d.Drink.Id == basketDrink.Drink.Id).AddCount();
            }

            orderListBox.ItemsSource = null;
            orderListBox.ItemsSource = Basket;
            basketLabel.Content = OrderPriceString;
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Guid drinkId = Guid.Parse(button.Tag.ToString());

            BasketDrink basketDrink = Basket.FirstOrDefault(i => i.Drink.Id == drinkId);
            if (basketDrink is null) return;

            Drinks.FirstOrDefault(d => d.Drink.Id == drinkId).AddCount(basketDrink.Count);
            Basket.Remove(basketDrink);

            orderListBox.ItemsSource = null;
            orderListBox.ItemsSource = Basket;
            basketLabel.Content = OrderPriceString;
        }

        private async void buyButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientMoney == 0) { App.ShowMessage("Внесите деньги"); return; }
            if (ClientMoney != 0 && OrderPrice == 0)
            {
                MachineGiveMoney(ClientMoney);
                App.ShowMessage($"Вы получили сдачу: {ClientMoney - OrderPrice} руб.");
                ClientMoney = 0;

                this.DataContext = null;
                this.DataContext = this;
                return; 
            }

            if (OrderPrice > ClientMoney)
            {
                App.ShowMessage($"Вашего баланса не хватает для покупки товаров.{Environment.NewLine}Не хватает: {OrderPrice - ClientMoney} руб.");
                return;
            }

            Boolean canGiveMoney = MachineGiveMoney(ClientMoney - OrderPrice);
            if (!canGiveMoney)
            {
                App.ShowMessage($"Аппарат не смог вернуть сдачу - не хватает монет внутри автомата{Environment.NewLine}" +
                    $"Попробуйте внести монеты с другим номиналом, чтобы автомат мог выдать сдачу.{Environment.NewLine}" +
                    $"Перед этим стоит очистить свою корзину и вернуть все ваши монеты внутри автомата:)");
                return;
            }

            await HttpHelper.SaveCoins(Coins
                .Select(c => new VMCoinBlank(c.Id, c.VendingMachineId, c.Coin.Id, c.Coin.Nominal, c.Count, c.IsActive))
                .ToArray());

            await HttpHelper.SaveDrinks(Drinks
                .Select(d => new VMDrinkBlank(d.Id, d.Drink.Name, d.Drink.Image, d.VendingMachineId, d.Drink.Id, d.Drink.Nominal, d.Count))
                .ToArray());

            App.ShowMessage($"Вы получили свои товары и сдачу: {ClientMoney - OrderPrice} руб.");


            Basket.Clear();
            ClientMoney = 0;

            orderListBox.ItemsSource = null;
            orderListBox.ItemsSource = Basket;
            this.DataContext = null;
            this.DataContext = this;
            basketLabel.Content = OrderPriceString;

            App.Base.LoadingStop();
        }

        private Boolean MachineGiveMoney(Int32 @return)
        {
            Int32 moneyLeft = @return;
            VMCoin[] oldCoins = Coins.Clone() as VMCoin[];

            GiveCoins(ref moneyLeft, 10);
            if (moneyLeft == 0) return true;

            GiveCoins(ref moneyLeft, 5);
            if (moneyLeft == 0) return true;

            GiveCoins(ref moneyLeft, 2);
            if (moneyLeft == 0) return true;

            GiveCoins(ref moneyLeft, 1);
            if (moneyLeft == 0) return true;

            Coins = oldCoins;
            return false;
        }

        private void GiveCoins(ref Int32 moneyLeft, Int32 nominal)
        {
            VMCoin coin = Coins.FirstOrDefault(c => c.Coin.Nominal == nominal);
            if (coin is not null && coin.Count != 0)
            {
                Int32 numberOfCoin10 = moneyLeft / coin.Coin.Nominal;
                if (coin.Count >= numberOfCoin10)
                {
                    moneyLeft -= coin.Coin.Nominal * numberOfCoin10;
                    coin.DecreaseCount(numberOfCoin10);
                }
                else
                {
                    moneyLeft -= coin.Coin.Nominal * coin.Count;
                    coin.DecreaseCount(coin.Count);
                }
            }
        }
    }

    internal class BasketDrink
    {
        public Drink Drink { get; set; }
        public Int32 Count { get; set; }
        public Int32 TotalPrice => Count * Drink.Nominal;

        public BasketDrink(Drink drink, Int32 count)
        {
            Drink = drink;
            Count = count;
        }
    }
}
