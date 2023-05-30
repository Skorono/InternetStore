using System.Windows;

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
        }
    }
}
