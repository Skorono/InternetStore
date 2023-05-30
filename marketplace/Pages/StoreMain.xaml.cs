using InternetStore.Controls;
using InternetStore.Controls.Builders;
using InternetStore.Controls.Resources;
using InternetStore.Controls.XAMLControls;
using InternetStore.ModelDB;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для StoreMain.xaml
    /// </summary>
    public partial class StoreMain : Page
    {
        private UserViewDto User = null!;
        public static ProductBasket Basket = null!;
        public ProductArea ProductList = null!;

        public UserViewDto CurrentUser => User;

        public StoreMain(UserViewDto userModel)
        {
            User = userModel;
            InitializeComponent();
            LoadBasketIcon();
            LoadProfileIcon();
            LoadProductArea();
            CreateBasket();
            ToolPanel.SearchBox.SetSearchHandler(SearchProduct);
            //ProductList.SelectSubCategory(2);
            //ProductList.SortByCost(0, 3400);
            Grid.SetColumn(ProductList, 1);
            Grid.SetRow(ProductList, 1);
            Grid.SetColumnSpan(ProductList, 3);
            Grid.SetRowSpan(ProductList, 3);
            grid.Children.Add(ProductList);
            grid.UpdateLayout();
        }

        private void CreateBasket()
        {
            Basket = new ProductBasket(User.Id);
        }

        private void LoadProfileIcon()
        {
            ToolPanel.ProfileIcon.Click += ProfileNavigate;
            ToolPanel.ProfileIcon.UserName = BaseProvider.DbContext.UserPersonalInfs.ToList()
                                         .Where(row => row.Id == User.Id).First().Name;
        }

        private void LoadProductArea()
        {
            ProductContainerBuilder ContainerBuilder = new ProductContainerBuilder();

            ContainerBuilder.SetProductHandler(AddToBasket);
            if (string.Join(" ", UserType.Adminstrator, UserType.Rentail).Split(" ").Contains(CurrentUser.RoleId))
                ContainerBuilder.EditionalAccept();
            ProductList = ContainerBuilder.Build();
            ProductList.LoadProducts();
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

        private void SearchProduct(object sender, RoutedEventArgs e)
        {
            ProductList.SearchByName(ToolPanel.SearchBox.InputText.Text);
        }


        private void ShowSubCategory(object sender, RoutedEventArgs e)
        {

        }

        private void ToBasket(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Basket);
        }
    }
}
