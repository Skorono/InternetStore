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
        private static SignUp _context = null!;

        private SignUp()
        {
            InitializeComponent();
        }

        public static SignUp GetInstance()
        {
            if (_context == null)
            {
                _context = new SignUp();
            }
            return _context;
        }

        private void OnSignUp(object sender, RoutedEventArgs e)
        {
            var login = (WatermarkTextBox?)UIHelper.FindUid(this, "LoginField");
            var password = (WatermarkPasswordBox?)UIHelper.FindUid(this, "PasswordField");

            var User = BaseProvider.DbContext.Users
                                .ToList()
                                .Where(user => (login.Text == user.Email) && (password.Password == user.Password.Decrypt()))
                                .FirstOrDefault();
            if (User != null)
            {
                var userDTO = BaseProvider.DbContext.UserViewDtos.ToList().Where(user => user.Id == User.Id).First();
                NavigationService.Navigate(StoreMain.GetInstance(userDTO));
            }
            else
                Xceed.Wpf.Toolkit.MessageBox.Show("Неверный логин или пароль");
        }

        private void ReturnToMainPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
    }
}
