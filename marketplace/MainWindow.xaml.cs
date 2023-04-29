using System.Windows;
using System.Windows.Controls;
using InternetStore.ModelBD;
using InternetStore.Pages;
using Microsoft.EntityFrameworkCore;

namespace InternetStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static InternetStoreContext dbContext = new InternetStoreContext();
        
        public MainWindow()
        {
            dbContext.Database.OpenConnection();
            InitializeComponent();
        }

        private void RegistrationNavigate(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Registration());
        }

        private void SignUpNavigate(object sender, RoutedEventArgs e)
        {

        }
    }
}
