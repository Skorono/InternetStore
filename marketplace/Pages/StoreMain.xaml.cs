﻿using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;
using InternetStore.ModelDB;
using InternetStore.Controls;
using InternetStore.Controls.XAMLControls;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для StoreMain.xaml
    /// </summary>
    public partial class StoreMain : Page
    {
        private UserViewDto User = null!;
        public static ProductBasket Basket = null!;
        public UserViewDto CurrentUser { get { return User; } }

        public StoreMain(UserViewDto userModel)
        {
            User = userModel;
            InitializeComponent();
            LoadBasketIcon();
            LoadProfileIcon();
            CreateBasket();
            ProductList.NotifyChangeHandler(AddToBasket);
            ProductList.SortBySubCategory(1);
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
