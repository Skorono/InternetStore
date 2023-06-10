using DocumentFormat.OpenXml.InkML;
using InternetStore.Controls;
using InternetStore.Controls.Builders;
using InternetStore.Controls.Resources;
using InternetStore.Controls.XAMLControls;
using InternetStore.Controls.XAMLControls.Icons;
using InternetStore.ModelDB;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для StoreMain.xaml
    /// </summary>
    public partial class StoreMain : Page
    {
        private static StoreMain _context = null!;
        private static UserViewDto User = null!;
        public static  ProductBasket Basket = null!;
        public static  ProductArea ProductList = null!;
        private static ToolBox ToolPanel = null!;
        private static SideBar SidePanel = null!;

        public static UserViewDto CurrentUser => User;

        private StoreMain(UserViewDto userModel)
        {
            User = userModel;
            InitializeComponent();
            Init();
            //ProductList.SelectSubCategory(2);
            //ProductList.SortByCost(0, 3400);
        }

        private void Init()
        {
            InitToolPanel();
            InitSideBar();
            CreateBasket();
            LoadBasketIcon();
            LoadProfileIcon();
            LoadProductArea();
            ToolPanel.SearchBox.SetSearchHandler(SearchProduct);
        }

        public static StoreMain GetInstance(UserViewDto userModel)
        {
            if (_context == null)
                _context = new StoreMain(userModel);
            User = userModel;
            if (CurrentUser.Photo != null)
                ToolPanel.ProfileIcon.UserIcon.Source = ImageManager.LoadImage(CurrentUser.Photo);
            else
                ToolPanel.ProfileIcon.UserIcon.Source = ImageManager.LoadImage(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "camera_200.png"));
            ToolPanel.ProfileIcon.UserName = BaseProvider.DbContext.UserPersonalInfs.ToList()
                                         .Where(row => row.Id == User.Id).First().Name;
            ToolPanel.BasketIcon.Count = Basket.ProductCount;
            return _context;
        }

        private void CreateBasket()
        {
            Basket = new ProductBasket(User.Id);
        }

        private void InitSideBar()
        {
            SidePanel = SideBar.GetInstance();
            SidePanel.CategoriesList.UpdateClickHandler(SortBySubCategory);
            Grid.SetRow(SidePanel, 1);
            Grid.SetColumn(SidePanel, 0);
            Grid.SetRowSpan(SidePanel, 4);
            grid.Children.Add(SidePanel);
        }

        private void InitToolPanel()
        {
            ToolPanel = ToolBox.GetInstance();
            Grid.SetRow(ToolPanel, 0);
            Grid.SetColumn(ToolPanel, 0);
            Grid.SetColumnSpan(ToolPanel, 4);
            ToolPanel.Margin = new Thickness(0, 0, 0, 1);
            ToolPanel.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2F2F2F")!;
            ToolPanel.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF1F1E1E")!;
            grid.Children.Add(ToolPanel);
        }

        private void LoadProfileIcon()
        {
            //ProfileIcon ProfileIcon = (ProfileIcon)FindName("ProfileIcon");
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
            bool editPermissions = (CurrentUser.RoleId == UserType.Adminstrator);
            NavigationService.Navigate(new ProductPage((AbsProductView)sender, editPermissions));
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

        private void ReloadProductList(object sender, RoutedEventArgs e)
        {
            ProductList.Load();
        }

        private void SortBySubCategory(object sender, RoutedEventArgs e)
        {
            var SubCategoryTextBtn = sender as TextBlockButton;

            if (SubCategoryTextBtn == null)
                return;

            foreach (var subCategory in BaseProvider.DbContext.SubCategories.ToList())
            {
                if (subCategory.Name == SubCategoryTextBtn.Text){
                    ProductList.Sort(subCategory.Id);
                }
            }
        }
    }
}
