using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace InternetStore.Controls.XAMLControls.Icons
{
    /// <summary>
    /// Логика взаимодействия для ProfileIcon.xaml
    /// </summary>
    public partial class ProfileIcon : ViewControl
    {

        #region [ Binding Fields ]

        private DependencyProperty usrName =
            DependencyProperty.Register("usrName", typeof(string), typeof(ProfileIcon));

        #endregion

        #region [ Binding Properties ]
        public string UserName
        {
            get => (string)GetValue(usrName);

            set
            {
                SetValue(usrName, value);
                NotifyPropertyChanged("UserName");
            }
        }

        #endregion

        public ProfileIcon()
        {
            InitializeComponent();
            IconSettings.Source = new BitmapImage(new Uri(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "settingIcon.png"), UriKind.Relative));
        }
    }
}
