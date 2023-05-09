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
        public static ProductBasket Basket = new ProductBasket();
        public List<Item> ItemList = new List<Item>();
        public UserViewDto CurrentUser { get { return User; } }

        public StoreMain(UserViewDto userModel)
        {
            InitializeComponent();
            User = userModel;
            ToolPanel.profileIcon.Click += ProfileNavigate;
            ToolPanel.profileIcon.UserName = BaseControl.DbContext.UserPersonalInfs.ToList()
                                         .Where(row => row.Id == User.Id).First().Name;
            foreach (var product in BaseControl.DbContext.Products.ToList())
            {
                Item newItem = new Item(product);
                newItem.Click += AddToBasket;
                ItemList.Add(newItem);
            }
            ListBox.ItemsSource = ItemList;
        }

        private void ProfileNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Profile.getInstance(CurrentUser));
        }

        private void AddToBasket(object sender, RoutedEventArgs e)
        {
            BasketItem BasketEl = new BasketItem(((Item)sender).ProductModel);
            BasketEl.Width = 500;
            Basket.Add(BasketEl);
        }

        private void ToBasket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Basket);
        }
    }
}
