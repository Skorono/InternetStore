using InternetStore.ModelDB;
using System.Windows.Controls;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfileEdit.xaml
    /// </summary>
    public partial class ProfileEdit : Page
    {
        private static ProfileEdit? _profileditpage;

        private ProfileEdit(UserViewDto user)
        {
            InitializeComponent();
        }

        public static ProfileEdit getInstance(UserViewDto user)
        {
            if (_profileditpage == null)
            {
                _profileditpage = new ProfileEdit(user);
            }
            return _profileditpage;
        }
    }
}
