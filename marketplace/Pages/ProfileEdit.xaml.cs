using InternetStore.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                _profileditpage= new ProfileEdit(user);
            }
            return _profileditpage;
        }
    }
}
