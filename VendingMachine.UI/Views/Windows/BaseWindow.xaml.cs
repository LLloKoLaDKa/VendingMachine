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
using System.Windows.Shapes;
using System.Windows.Threading;
using VendingMachine.Domain;
using VendingMachine.UI.Tools;
using FA = FontAwesome.WPF;

namespace VendingMachine.UI.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {
            InitializeComponent();
            App.Current.Dispatcher.UnhandledException += ExceptionHandler;

            App.ChangeToSalePage();
        }

        public async Task LoadVendingMachine()
        {
            VendingMachineDomain machine = await HttpHelper.GetVendingMachine(Guid.Parse("ea349936-28a9-4f6d-84cc-a79c55ef55d4"));
            App.SetVendingMachine(machine);
        }

        private void ExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs args) =>
            MessageBox.Show("Произошла непредвиденная ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        public void LoadingRun()
        {
            loadingIcon.Visibility = Visibility.Visible;
        }

        public void LoadingStop()
        {
            loadingIcon.Visibility = Visibility.Hidden;
        }
    }
}
