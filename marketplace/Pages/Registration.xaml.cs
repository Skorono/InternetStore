using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Data.SqlClient;
using Xceed.Wpf.Toolkit;
using System.Windows.Navigation;
using InternetStore.Controls;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

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

        #region [ Binding Fields]
        private DependencyProperty username =
              DependencyProperty.Register("UserName", typeof(string), typeof(Registration));

        private DependencyProperty email =
              DependencyProperty.Register("Email", typeof(string), typeof(Registration));

        private DependencyProperty password =
              DependencyProperty.Register("Password", typeof(string), typeof(Registration));

        private DependencyProperty repeatpassword =
              DependencyProperty.Register("RepeatPassword", typeof(string), typeof(Registration));
        #endregion

        #region [ Binding Properties ]
        [Required(AllowEmptyStrings = false)]
        public string UserName { private get { return (string)GetValue(username); } 
                                 set { SetValue(username, value); } }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Невалидная почта!")]
        public string Email { private get { return (string)GetValue(email); } 
                              set { SetValue(email, value); } }

        [Required(AllowEmptyStrings = false)]
        [PasswordPropertyText]
        public string Password { private get { return (string)GetValue(password); } 
                                 set { SetValue(password, value); } }
        
        [Required(AllowEmptyStrings = false)]
        [Compare("Password")]
        public string RepeatedPassword { private get { return (string)GetValue(repeatpassword); }
                                         set { SetValue(repeatpassword, value); } }
        #endregion

        public Registration()
        {
            InitializeComponent();
        }

        public void SelectImageCommand(object sender, DragEventArgs e) {
            var dlg = new OpenFileDialog();
            //if (dlg.ShowDialog() == true)
                //UsrAvatar = new Image().Upload();
        }

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword()
        {
            var context = new ValidationContext(this);
            var errors = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (!Validator.TryValidateProperty(Password, context, errors))
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Слишком простой пароль!");
            }
            if (!Validator.TryValidateProperty(RepeatedPassword, context, errors))
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Пароли не совпадают!");
            }
            return true;
        }

        private void Registrastration(object sender, RoutedEventArgs e)
        {
            //if (IsValidPassword())
            //{
                SqlParameter email = new SqlParameter("email", Email);
                SqlParameter name = new SqlParameter("name", UserName);
                SqlParameter password = new SqlParameter("password", ((WatermarkPasswordBox?)UIHelper.FindUid(this, "Password")).Password);
                BaseProvider.CallStoredProcedureByName("AddUser", email, password, name);
                var userDTO = BaseProvider.DbContext.UserViewDtos.ToList().Where(user => user.Email == email.Value.ToString()).First();
                NavigationService.Navigate(new StoreMain(userDTO));
            //}
        }
    }
}
