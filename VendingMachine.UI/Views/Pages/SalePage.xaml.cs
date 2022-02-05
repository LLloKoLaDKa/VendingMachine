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
        public String OrderPrice => $"Корзина({Basket.Select(i => i.TotalPrice).Sum()} р.)";

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
            ClientMoney += coin.Coin.Nominal;
            this.DataContext = null;
            this.DataContext = this;
        }

        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            App.Base.LoadingRun();

            AuthorizeWindow window = new();
            window.Show();

            App.Base.LoadingStop();
        }

        private void drinksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            VMDrink vmDrink = listBox.SelectedItem as VMDrink;
            if (vmDrink is null) return;

            BasketDrink? basketDrink = Basket.FirstOrDefault(i => i.Drink.Id == vmDrink.Drink.Id);
            if (basketDrink is null) Basket.Add(new BasketDrink(vmDrink.Drink, 1));
            else
            {
                basketDrink.Count++;
            }

            orderListBox.ItemsSource = null;
            orderListBox.ItemsSource = Basket;
            basketLabel.Content = OrderPrice;

            drinksListBox.SelectedIndex = -1;
        }

        private void DecreaseItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Guid drinkId = Guid.Parse(button.Tag.ToString());

            BasketDrink basketDrink = Basket.FirstOrDefault(i => i.Drink.Id == drinkId);
            if (basketDrink is null) return;

            if (basketDrink.Count == 1) RemoveItem_Click(sender, e);
            else basketDrink.Count--;

            orderListBox.ItemsSource = null;
            orderListBox.ItemsSource = Basket;
            basketLabel.Content = OrderPrice;
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            Guid drinkId = Guid.Parse(button.Tag.ToString());

            BasketDrink basketDrink = Basket.FirstOrDefault(i => i.Drink.Id == drinkId);
            if (basketDrink is null) return;

            Basket.Remove(basketDrink);

            orderListBox.ItemsSource = null;
            orderListBox.ItemsSource = Basket;
            basketLabel.Content = OrderPrice;
        }

        private void buyButton_Click(object sender, RoutedEventArgs e)
        {
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
