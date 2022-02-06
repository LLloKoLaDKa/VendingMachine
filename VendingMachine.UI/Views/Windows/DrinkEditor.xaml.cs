using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using VendingMachine.Domain.Drinks;
using VendingMachine.Domain.Results;
using VendingMachine.UI.Tools;

namespace VendingMachine.UI.Views.Windows
{
    /// <summary>
    /// Interaction logic for DrinkEditor.xaml
    /// </summary>
    public partial class DrinkEditor : Window
    {
        public VMDrinkBlank Drink { get; set; }

        public DrinkEditor(VMDrinkBlank? drink = null)
        {
            InitializeComponent();

            Drink = drink ?? new(Guid.NewGuid(), null, new Byte[0], null, null, null, null);
            SetImage();

            this.DataContext = Drink;
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из редактора? Все несохранённые данные будут потеряны!",
                "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;

            Close();
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingRun();

            Drink.VendingMachineId = App.VendingMachine.Id;
            Drink.DrinkId = Drink.DrinkId ?? Guid.NewGuid();
            Result result = await HttpHelper.SaveDrink(Drink);
            if (!result.IsSuccess)
            {
                App.ShowMessage(result.Error);
                LoadingStop();
                return;
            }

            if (Owner is not null) Owner.Activate();
            Close();
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingStop();

            MessageBoxResult messageBoxResult = App.ShowMessage($"Вы уверены, что хотите удалить напиток {Drink.Name}?", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            Result result = await HttpHelper.DeleteDrink(Drink.Id.Value);
            if (!result.IsSuccess)
            {
                App.ShowMessage(result.Error);
            }

            App.ShowMessage("Напиток успешно удалён");
            Close();

            LoadingStop();
        }

        private async void imageButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingRun();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG Image|*.png";

            if (ofd.ShowDialog().Value)
            {
                String fileName = ofd.FileName;
                Drink.Image = await File.ReadAllBytesAsync(fileName);

                SetImage();
            }

            LoadingStop();
        }

        private void SetImage()
        {
            if (Drink.Image.Length == 0) return;

            BitmapImage image = new BitmapImage();
            using (MemoryStream stream = new(Drink.Image))
            {
                stream.Position = 0;
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }

            imageBox.Source = null;
            imageBox.Source = image;
        }

        public void LoadingRun()
        {
            loadingIcon.Visibility = Visibility.Visible;
        }

        public void LoadingStop()
        {
            loadingIcon.Visibility = Visibility.Hidden;
        }

        private void NumberInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.Text[0]);
        }
    }
}
