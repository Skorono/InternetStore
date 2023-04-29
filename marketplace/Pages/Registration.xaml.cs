using Microsoft.Win32;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.IO.Packaging;
using System.Windows.Navigation;

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
            //if (dlg.ShowDialog() == true)
                //UsrAvatar = new Image().Upload();
        }

        public static UIElement? FindUid(DependencyObject parent, string uid)
        {
            UIElement? el = new UIElement();
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                el = VisualTreeHelper.GetChild(parent, i) as UIElement;
                if (el != null)
                {
                    if (el?.Uid == uid)
                        return el;
                    el = FindUid(el, uid);
                    if (el != null) return el;
                }
            }
            return null;
        }

        private void Registrastration(object sender, RoutedEventArgs e)
        {
            var email = new SqlParameter("email", ((WatermarkTextBox?)(FindUid(this, "EmailField")))?.Text);
            var name = new SqlParameter("name", ((WatermarkTextBox?)(FindUid(this, "NameField")))?.Text);
            var password = new SqlParameter("password", ((WatermarkPasswordBox?)(FindUid(this, "PasswordField")))?.Text);
            MainWindow.dbContext.Database.ExecuteSqlRaw("EXEC AddUser @email, @password, @name", parameters: new[] { email, password, name });
            NavigationService.Navigate(new StoreMain());
        }
    }
}
