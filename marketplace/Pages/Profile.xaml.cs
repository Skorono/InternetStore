using System.Windows;
using System.Windows.Controls;
using InternetStore.Pages;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Profile()
        {

            InitializeComponent();
        }

        private void OnMainPage(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Hyperlink_RequestNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfileEdit());
        }
    }
}
