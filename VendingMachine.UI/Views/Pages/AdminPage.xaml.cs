using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using VendingMachine.Domain.Reports;
using VendingMachine.Domain.Results;
using VendingMachine.UI.Tools;
using VendingMachine.UI.Views.Windows;

namespace VendingMachine.UI.Views.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public VMCoinBlank[] Coins { get; set; }
        public VMDrinkBlank[] Drinks { get; set; }
        public DrinkReport[] Reports { get; set; }

        public AdminPage()
        {
            InitializeComponent();

            tabControl.SelectedIndex = 0;
            this.DataContext = this;
        }

        private async Task LoadCoins()
        {
            App.Base.LoadingRun();

            VMCoin[] coins = await HttpHelper.GetVmCoins(App.VendingMachine.Id);
            Coins = coins
                .Select(c => new VMCoinBlank(c.Id, c.VendingMachineId, c.Coin.Id, c.Coin.Nominal, c.Count, c.IsActive))
                .ToArray();

            coinsListBox.ItemsSource = null;
            coinsListBox.ItemsSource = Coins;

            App.Base.LoadingStop();
        }

        private async Task LoadDrinks()
        {
            App.Base.LoadingRun();

            VMDrink[] drinks = await HttpHelper.GetVmDrinks(App.VendingMachine.Id);
            Drinks = drinks
                .Select(d => new VMDrinkBlank(d.Id, d.Drink.Name, d.Drink.Image, d.VendingMachineId, d.Drink.Id, d.Drink.Nominal, d.Count))
                .OrderBy(d => d.Nominal)
                .ToArray();

            drinksListBox.ItemsSource = null;
            drinksListBox.ItemsSource = Drinks;

            App.Base.LoadingStop();
        }

        private async Task LoadReports()
        {
            App.Base.LoadingRun();

            VMDrink[] drinks = await HttpHelper.GetVmDrinks(App.VendingMachine.Id);
            Reports = await HttpHelper.GetDrinkReports(drinks.Select(d => d.Drink.Id).ToArray());

            reportsBox.ItemsSource = null;
            reportsBox.ItemsSource = Reports;

            App.Base.LoadingStop();
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
            await LoadCoins();
        }

        private async void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabs = sender as TabControl;
            switch (tabs.SelectedIndex)
            {
                case 0: await LoadCoins(); break;
                case 1: await LoadDrinks(); break;
                case 2: await LoadReports(); break;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e) => ShowEditor();

        private void drinksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            VMDrinkBlank item = listBox.SelectedItem as VMDrinkBlank;
            if (item is null) return;

            ShowEditor(item);
        }

        private async void ShowEditor(VMDrinkBlank? blank = null)
        {
            App.Base.LoadingRun();

            DrinkEditor editor = new(blank);
            editor.Owner = App.Base;
            editor.ShowDialog();

            App.Base.LoadingStop();
            await LoadDrinks();
        }
    }
}
