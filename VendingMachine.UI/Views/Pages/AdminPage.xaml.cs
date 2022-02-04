using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using VendingMachine.Domain.Results;
using VendingMachine.UI.Tools;

namespace VendingMachine.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public VMCoinBlank[] Coins { get;set; }

        public AdminPage()
        {
            InitializeComponent();
            LoadCoins();
            this.DataContext = this;
        }

        private async void LoadCoins()
        {
            App.Base.LoadingRun();

            VMCoin[] coins = await HttpHelper.GetVmCoins(App.VendingMachine.Id);
            Coins = coins
                .Select(c => new VMCoinBlank(c.Id, c.VendingMachineId, c.Coin.Id, c.Coin.Nominal, c.Count, c.IsActive))
                .ToArray();

            coinsListView.ItemsSource = null;
            coinsListView.ItemsSource = Coins;

            App.Base.LoadingStop();
        }

        private async void LoadDrinks()
        {
            App.Base.LoadingRun();

            VMDrink drinks = await HttpHelper.GetVmDrinks(App.VendingMachine.Id);
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeToSalePage();
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            App.Base.LoadingRun();
            Result result = await HttpHelper.SaveCoins(Coins);
            if (!result.IsSuccess)
            {
                App.ShowMessage(result.Error);
                return;
            }
            LoadCoins();
        }
    }
}
