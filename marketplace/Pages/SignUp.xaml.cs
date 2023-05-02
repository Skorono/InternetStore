using InternetStore.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Diagnostics;
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
            
            var startTime = Stopwatch.StartNew();
            var user = BaseControl.DbContext.Users
                                .Where(user => (login.Text == user.Email) && (password.Password == user.Password))
                                .FirstOrDefault();
            if (user != null)
            {
                NavigationService.Navigate(new StoreMain());
               startTime.Stop();
               Xceed.Wpf.Toolkit.MessageBox.Show(string.Format(" Затраченное время в секундах {0}.{1}",
                                        startTime.Elapsed.Seconds,
                                        startTime.Elapsed.Milliseconds
                                        ));
            }
            else
                Xceed.Wpf.Toolkit.MessageBox.Show("Неверный логин или пароль");
        }
    }
}
