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

namespace SoapDemo2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowViewModel();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonSendRequest.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;

            var viewModel = (MainWindowViewModel)DataContext;
            try
            {
                await viewModel.SendRequest();
                MessageBox.Show("Receiving data completed!", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occurred!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ButtonSendRequest.IsEnabled = true;
            Mouse.OverrideCursor = null;
        }
    }
}
