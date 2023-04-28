using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private BitmapImage UsrAvatar {
            get { return UsrAvatar; }
            set { UsrAvatar = value; }
        }

        public Registration()
        {
            InitializeComponent();
        }

        public void SelectImageCommand(object sender, DragEventArgs e) {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
                UsrAvatar = new BitmapImage(new Uri(dlg.FileName, UriKind.Relative));
        }

        private void Registrastration(object sender, RoutedEventArgs e)
        {
            DataContext.
            NavigationService.Navigate(new StoreMain());
        }
    }
}
