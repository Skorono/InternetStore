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
        private static ProfileIcon _context = null!;
        private ProfileIcon()
        {
            InitializeComponent();
            IconSettings.Source = ImageManager.LoadImage(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "settingIcon.png"));
            IconSettings.UpdateLayout();
        }

        public static ProfileIcon GetInstance()
        {
            if (_context == null)
            {
                _context = new ProfileIcon();
            }
            return _context;
        }
    }
}
