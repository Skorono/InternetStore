using InternetStore.Controls;
using InternetStore.Controls.Builders;
using InternetStore.Controls.Resources;
using InternetStore.Controls.XAMLControls;
using InternetStore.ModelDB;
using System;
using System.IO;
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
            CreateBasket();
            LoadBasketIcon();
            LoadProfileIcon();
            LoadProductArea();
            ToolPanel.SearchBox.SetSearchHandler(SearchProduct);
            //ProductList.SelectSubCategory(2);
            //ProductList.SortByCost(0, 3400);

        }

        private void CreateBasket()
        {
            Basket = new ProductBasket(User.Id);
        }

        private void LoadProfileIcon()
        {
            ToolPanel.ProfileIcon.Click += ProfileNavigate;
            if (CurrentUser.Photo != null)
                ToolPanel.ProfileIcon.UserIcon.Source = ImageManager.LoadImage(CurrentUser.Photo);
            else
                ToolPanel.ProfileIcon.UserIcon.Source = ImageManager.LoadImage(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "camera_200.png"));

            ToolPanel.ProfileIcon.UserName = BaseProvider.DbContext.UserPersonalInfs.ToList()
                                         .Where(row => row.Id == User.Id).First().Name;
            ToolPanel.BasketIcon.Count = Basket.ProductCount;
        }

        private void LoadProductArea()
        {


            ProductContainerBuilder ContainerBuilder = new ProductContainerBuilder();

            ContainerBuilder.SetProductClickHandler(AddToBasket);
            ContainerBuilder.SetProductDoubleClickHandler(ToProductPage);

            switch (CurrentUser.RoleId)
            {
                case UserType.Rentail:
                    ContainerBuilder.RentalPermissions();
                    break;

                case UserType.Adminstrator:
                    ContainerBuilder.AdministratorPermissions();
                    break;
            }
            ProductList = ContainerBuilder.Build();

            Grid.SetColumn(ProductList, 1);
            Grid.SetRow(ProductList, 1);
            Grid.SetColumnSpan(ProductList, 3);
            Grid.SetRowSpan(ProductList, 3);
            grid.Children.Add(ProductList);
            grid.UpdateLayout();

            ProductList.Load();
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

        private void ToProductPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductPage((AbsProductView)sender));
        }

        private void SearchProduct(object sender, RoutedEventArgs e)
        {
            ProductList.Sort(ToolPanel.SearchBox.InputText.Text);
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
