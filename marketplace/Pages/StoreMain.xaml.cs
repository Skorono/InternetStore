using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using InternetStore.Controls;
using InternetStore.ModelBD;
using System.Windows;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для StoreMain.xaml
    /// </summary>
    public partial class StoreMain : Page
    {
        private User User;
        public List<Item> ItemList = new List<Item>();

        public User CurrentUser { get { return User; } }

        public StoreMain(User user)
        {
            InitializeComponent();
            User = user;
            ToolPanel.profileIcon.Click += ProfileNavigate;
            ToolPanel.profileIcon.UserName = BaseControl.DbContext.UserPersonalInfs.ToList()
                                         .Where(row => row.Id == User.Id).First().Name;
            foreach (var item in BaseControl.DbContext.Products.ToList())
            {
                ItemList.Add(new Item(item.ProductName, 365f));
            }
            ListBox.ItemsSource = ItemList;
        }

        private void ProfileNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Profile());
        }

    }
}
