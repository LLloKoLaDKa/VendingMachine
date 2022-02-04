using System.Windows;
using VendingMachine.Domain;
using VendingMachine.UI.Views.Pages;
using VendingMachine.UI.Views.Windows;

namespace VendingMachine.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static VendingMachineDomain VendingMachine;
        public static BaseWindow Base => (Current.MainWindow as BaseWindow);

        public static void SetVendingMachine(VendingMachineDomain vendingMachine) => VendingMachine = vendingMachine;

        public static void ChangeToAdminPage()
        {
            Base.mainFrame.Content = new AdminPage();
        }

        public static void ChangeToSalePage()
        {
            Base.mainFrame.Content = new SalePage();
        }

        public static void ShowMessage(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
