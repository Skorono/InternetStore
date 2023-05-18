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
        private UserViewDto User = null!;
        public static ProductBasket Basket = null!;
        public List<Item> ItemList = new List<Item>();
        public UserViewDto CurrentUser { get { return User; } }

        public StoreMain(UserViewDto userModel)
        {
            User = userModel;
            InitializeComponent();
            LoadProducts();
            LoadBasketIcon();
            LoadProfileIcon();
            CreateBasket();
        }

        private void CreateBasket()
        {
            Basket = new ProductBasket(User.Id);
        }

        private void LoadProducts()
        {
            foreach (var product in BaseProvider.DbContext.Products.ToList())
            {
                Item newItem = new Item(product);
                newItem.Click += AddToBasket;
                ItemList.Add(newItem);
            }
            ListBox.ItemsSource = ItemList;
        }

        private void LoadProfileIcon()
        {
            ToolPanel.ProfileIcon.Click += ProfileNavigate;
            ToolPanel.ProfileIcon.UserName = BaseProvider.DbContext.UserPersonalInfs.ToList()
                                         .Where(row => row.Id == User.Id).First().Name;
        }

        private void LoadBasketIcon()
        {
            ToolPanel.BasketIcon.DoubleClick += ToBasket;
        }

        private void ProfileNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Profile.getInstance(CurrentUser));
        }

        private void AddToBasket(object sender, RoutedEventArgs e)
        {
            BasketItem BasketEl = new BasketItem(((Item)sender).ProductModel);
            BasketEl.Width = 525;
            Basket.Add(BasketEl);
            ToolPanel.BasketIcon.Count = Basket.ProductCount;
        }

        private void ToBasket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Basket);
        }
    }
}
