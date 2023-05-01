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
using InternetStore.Controls;

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

        private void Registrastration(object sender, RoutedEventArgs e)
        {
            var email = new SqlParameter("email", 
                ((WatermarkTextBox?)(UIHelper.FindUid(this, "EmailField")))?.Text);
            
            var name = new SqlParameter("name", 
                ((WatermarkTextBox?)(UIHelper.FindUid(this, "NameField")))?.Text);
            
            var password = new SqlParameter("password", 
                ((WatermarkPasswordBox?)(UIHelper.FindUid(this, "PasswordField")))?.Password);
            
            BaseControl.CallStoredProcedureByName("AddUser", email, password, name);
            NavigationService.Navigate(new StoreMain());
        }
    }
}
