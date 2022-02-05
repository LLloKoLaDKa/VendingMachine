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
using VendingMachine.Domain.Results;
using VendingMachine.UI.Tools;

namespace VendingMachine.UI.Views.Windows
{
    /// <summary>
    /// Interaction logic for AuthorizeWindow.xaml
    /// </summary>
    public partial class AuthorizeWindow : Window
    {
        public AuthorizeWindow()
        {
            InitializeComponent();
            codeBox.Focus();
        }

        private async void loginBox_Click(object sender, RoutedEventArgs e)
        {
            Result result = await HttpHelper.LoginInVendingMachine(App.VendingMachine.Id, codeBox.Text);
            if (!result.IsSuccess)
            {
                App.ShowMessage(result.Error);
                return;
            }

            App.ChangeToAdminPage();
            App.Base.Activate();
            Close();
        }
    }
}
