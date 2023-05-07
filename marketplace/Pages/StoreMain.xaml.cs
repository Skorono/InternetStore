using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using InternetStore.Controls;
using System.Windows;
using InternetStore.ModelDB;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для StoreMain.xaml
    /// </summary>
    public partial class StoreMain : Page
    {
        private UserViewDto User;
        public List<Item> ItemList = new List<Item>();
        public UserViewDto CurrentUser { get { return User; } }

        public StoreMain(UserViewDto userModel)
        {
            InitializeComponent();
            User = userModel;
            ToolPanel.profileIcon.Click += ProfileNavigate;
            ToolPanel.profileIcon.UserName = BaseControl.DbContext.UserPersonalInfs.ToList()
                                         .Where(row => row.Id == User.Id).First().Name;
            foreach (var item in BaseControl.DbContext.Products.ToList())
            {
                ItemList.Add(new Item(item));
            }
            ListBox.ItemsSource = ItemList;
        }

        private void ProfileNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Profile.getInstance(CurrentUser));
        }
    }
}
