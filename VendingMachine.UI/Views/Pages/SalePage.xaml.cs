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
using VendingMachine.UI.Tools;

namespace VendingMachine.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for SalePage.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        public Int32 ClientMoney { get; set; } = 0;
        public VMCoin[] Coins { get; private set; }

        public String ClientMoneyString => $"Вложенная сумма: {ClientMoney} руб.";

        public SalePage()
        {
            InitializeComponent();
            LoadData();
            this.DataContext = this;
        }

        public async void LoadData()
        {
            App.Base.LoadingRun();

            if (App.VendingMachine is null) await App.Base.LoadVendingMachine();

            Coins = await HttpHelper.GetVmCoins(App.VendingMachine.Id);
            coinsListView.ItemsSource = null;
            coinsListView.ItemsSource = Coins;

            App.Base.LoadingStop();
        }

        private void Money_Click(object sender, RoutedEventArgs e)
        {
            VMCoin coin = (sender as Button).DataContext as VMCoin;
            ClientMoney += coin.Coin.Nominal;
            moneyLabel.DataContext = null;
            moneyLabel.DataContext = this;
        }

        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeToAdminPage();
        }
    }
}
