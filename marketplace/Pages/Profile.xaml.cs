using InternetStore.Controls;
using InternetStore.ModelDB;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page, INotifyPropertyChanged
    {
        private UserViewDto User { get; set; }
        private static Profile? _profilepage;

        #region [ Binding Fields ]
        private DependencyProperty name = DependencyProperty.Register(
            "UserName", typeof(string), typeof(Profile));

        private DependencyProperty email = DependencyProperty.Register(
            "Email", typeof(string), typeof(Profile));

        private DependencyProperty surname = DependencyProperty.Register(
            "Surname", typeof(string), typeof(Profile));

        private DependencyProperty middleName = DependencyProperty.Register(
            "MiddleName", typeof(string), typeof(Profile));

        private DependencyProperty photo = DependencyProperty.Register(
            "Photo", typeof(byte[]), typeof(Profile));
        #endregion

        #region [ Binding Properties ]
        public string UserName
        {
            get => (string)GetValue(name);
            set => SetValue(name, value);
        }
        public string Email
        {
            get => (string)GetValue(email);
            set => SetValue(email, value);
        }
        public string? Surname
        {
            get => (string)GetValue(surname);
            set => SetValue(surname, value);
        }
        public string? MiddleName
        {
            get => (string)GetValue(middleName);
            set => SetValue(middleName, value);
        }
        public byte[]? Photo
        {
            get => (byte[])GetValue(photo);
            set => SetValue(photo, value);
        }
        #endregion

        private Profile(UserViewDto user)
        {
            User = user;
            UserName = User.Name;
            Surname = User.Surname;
            MiddleName = User.MiddleName;
            Photo = User.Photo;
            if (Photo == null)
                Photo = ImageManager.Upload(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "camera_200.png"));

            InitializeComponent();
        }

        public static Profile getInstance(UserViewDto user)
        {
            if (_profilepage == null)
            {
                _profilepage = new Profile(user);
            }
            return _profilepage;
        }

        private void OnMainPage(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Hyperlink_EditProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ProfileEdit.getInstance(User));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
