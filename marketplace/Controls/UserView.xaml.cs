using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using InternetStore.ModelDB;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : UserControl, INotifyPropertyChanged
    {
        private User UserModel;

        #region [ Binding Fields ]
        private DependencyProperty name = DependencyProperty.Register(
            "UserName", typeof(string), typeof(UserView));

        private DependencyProperty email = DependencyProperty.Register(
            "Email", typeof(string), typeof(UserView));

        private DependencyProperty password = DependencyProperty.Register(
            "Password", typeof(string), typeof(UserView));
        #endregion

        #region [ Binding Properties ]
        public string UserName { get => (string)GetValue(name);
            set { SetValue(name, value); NotifyPropertyChanged("UserName"); } }
        public string Email { get => (string)GetValue(email);
            set { SetValue(email, value); NotifyPropertyChanged("Email"); } }
        public string Password { get => (string)GetValue(password);
            set { SetValue(password, value); NotifyPropertyChanged("Password"); } }
        #endregion

        public UserView(User model)
        {
            UserModel = model;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
