using InternetStore.Controls;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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

            var User = BaseProvider.DbContext.Users
                                .Where(user => (login.Text == user.Email) && (password.Password == user.Password))
                                .FirstOrDefault();
            if (User != null)
            {
                var userDTO = BaseProvider.DbContext.UserViewDtos.ToList().Where(user => user.Id == User.Id).First();
                NavigationService.Navigate(new StoreMain(userDTO));
            }
            else
                Xceed.Wpf.Toolkit.MessageBox.Show("Неверный логин или пароль");
        }
    }
}
