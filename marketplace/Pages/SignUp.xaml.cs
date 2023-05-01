using InternetStore.Controls;
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
using Xceed.Wpf.Toolkit;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void OnSignUp(object sender, RoutedEventArgs e)
        {
            var login = (WatermarkTextBox?)UIHelper.FindUid(this, "LoginField");
            var password = (WatermarkPasswordBox?)UIHelper.FindUid(this, "PasswordField");
            
            foreach (var user in BaseControl.DbContext.Users.ToList())
            {
                if ((login.Text == user.Email) && (password.Password == user.Password))
                {
                    NavigationService.Navigate(new StoreMain());
                }
            }
            Xceed.Wpf.Toolkit.MessageBox.Show("Неверный логин или пароль");
        }
    }
}
