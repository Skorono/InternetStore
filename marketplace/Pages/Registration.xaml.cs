using DocumentFormat.OpenXml.EMMA;
using InternetStore.Controls;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Xceed.Wpf.Toolkit;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {

        #region [ Binding Fields]
        private DependencyProperty userNameField =
              DependencyProperty.Register("UserName", typeof(string), typeof(Registration));

        private DependencyProperty emailField =
              DependencyProperty.Register("Email", typeof(string), typeof(Registration));

        private DependencyProperty passwordField =
              DependencyProperty.Register("Password", typeof(string), typeof(Registration));

        private DependencyProperty repeatPasswordField =
              DependencyProperty.Register("RepeatPassword", typeof(string), typeof(Registration));
        #endregion

        #region [ Binding Properties ]
        [Required(AllowEmptyStrings = false)]
        public string UserName
        {
            get => (string)GetValue(userNameField);
            set => SetValue(userNameField, value);
        }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Невалидная почта!")]
        public string Email
        {
            get => (string)GetValue(emailField);
            set => SetValue(emailField, value);
        }

        //[Required(AllowEmptyStrings = false)]
        [PasswordPropertyText]
        public string Password
        {
            get => (string)GetValue(passwordField);
            set => SetValue(passwordField, value);
        }

        [Required(AllowEmptyStrings = false)]
        [Compare("Password")]
        public string RepeatedPassword
        {
            get => (string)GetValue(repeatPasswordField);
            set => SetValue(repeatPasswordField, value);
        }
        #endregion

        private static Registration _context = null!;

        private Registration()
        {
            InitializeComponent();
        }

        public static Registration GetInstance()
        {
            if (_context == null)
            {
                _context = new Registration();
            }
            return _context;
        }

        private bool isValid(object property, string propertyName)
        {
            var errors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            Validator.TryValidateProperty(property, new ValidationContext(this, null, null) { MemberName = propertyName }, errors);

            return errors.IsNullOrEmpty();
        }

        private bool IsValidEmail()
        {
            bool result = isValid(Email, nameof(Email));
            if (!result)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Некорректный адрес электронной почты!");
            }

            return result;
        }

        private bool IsValiUserName()
        {
            bool result = isValid(UserName, nameof(UserName));
            if (!result)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Некорректное имя пользователя!");
            }

            return result;
        }


        private bool IsValidPassword()
        {
            
            if (!isValid(Password, nameof(Password)))
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Слишком простой пароль!"); return false;
            }
            else if (!isValid(RepeatedPassword, nameof(RepeatedPassword)))
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Пароли не совпадают!"); return false;
            }
            return true;
        }

        private bool Validate()
        {
            return (IsValiUserName() && IsValidEmail() && IsValidPassword());
        }

        private void ReturnToMainPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        private bool UserNotExists()
        {
            if (Email != null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Пользователь с такой почтой уже существует");
                return BaseProvider.DbContext.Users.Where(user => user.Email == Email).IsNullOrEmpty();
            }
            return false;
        }

        private void Registrastration(object sender, RoutedEventArgs e)
        {
            Password = ((WatermarkPasswordBox?)UIHelper.FindUid(this, "Password"))!.Password;
            RepeatedPassword = ((WatermarkPasswordBox?)UIHelper.FindUid(this, "Password"))!.Password;

            if (Validate() && UserNotExists())
            {
                SqlParameter email = new SqlParameter("email", Email);
                SqlParameter name = new SqlParameter("name", UserName);
                SqlParameter password = new SqlParameter("password", Password.Encrypt());
                BaseProvider.CallStoredProcedureByName("AddUser", email, password, name);

                var userDTO = BaseProvider.DbContext.UserViewDtos.ToList().Where(user => user.Email == Email).First();
                userDTO.Photo = ImageManager.ImageSourceToBytes(new TiffBitmapEncoder(), UserIcon.Source);
                NavigationService.Navigate(StoreMain.GetInstance(userDTO));
            }
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            UserIcon.Source = ImageManager.LoadImageFromFileDialog();
        }
    }
}
