using InternetStore.Pages;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore
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

        private void RegistrationNavigate(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Registration());
        }

        private void SignUpNavigate(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new SignUp());
        }
    }
}
