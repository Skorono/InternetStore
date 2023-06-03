﻿using InternetStore.Controls;
using InternetStore.Controls.Interfaces;
using InternetStore.Controls.XAMLControls;
using InternetStore.ModelDB;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductBasket.xaml
    /// </summary>
    public partial class ProductBasket : Page
    {
        private int UserId;
        private List<IBasketViewItem> Products = new List<IBasketViewItem>();

        public int ProductCount => Products.Count;

        public ProductBasket(int UID)
        {
            UserId = UID;
            InitializeComponent();
            foreach (var basketProduct in BaseProvider.DbContext.Baskets
                                                    .ToList()
                                                    .Where(userBasket => userBasket.UserId == UserId))
            {
                // Добавить настройку вида продукта в билдере корзины
                Product model = BaseProvider.DbContext.Products
                            .ToList()
                            .Where(product => product.Id == basketProduct.ProductId)
                            .First();

                BasketItem basketItem = new(model);
                basketItem.Width = 525;
                //basketItem.DeleteBtn.Click += RemoveItem;
                Products.Add(basketItem);
                NotifyBasketChange();
            }
        }

        private void NotifyBasketChange()
        {
            if (Products.Count > 0)
                BasketList.ItemsSource = Products;
        }

        public bool inBasket(int ID)
        {
            return !Products.Where(product => product.ProductModel.Id == ID).IsNullOrEmpty();
        }

        private void CreateBasketEntitry()
        {

        }

        public void Add(IBasketViewItem product)
        {
            SqlParameter uid = new SqlParameter("user_id", UserId);
            SqlParameter productId = new SqlParameter("product_id", product.ProductModel.Id);
            SqlParameter count = new SqlParameter("count", product.Count);

            if (inBasket(product.ProductModel.Id))
            {
                var ProductInBasket = Products.Where(Bproduct => Bproduct.ProductModel.Id == product.ProductModel.Id).First();
                ProductInBasket.Count++;
                count.Value = ProductInBasket.Count;
                BaseProvider.CallStoredProcedureByName("UpdateProductCountInBasket", uid, productId, count);
            }
            else
            {
                SqlParameter AddingDateTime = new SqlParameter("addDate", DateTime.Now);
                BaseProvider.CallStoredProcedureByName("AddProductToBasket", uid, productId, count, AddingDateTime);
                Products.Add(product);
            }
            //BaseControl.DbContext.Baskets.Add();
            NotifyBasketChange();
        }

        public void Remove(IBasketViewItem product)
        {
            product.Count--;
            SqlParameter uid = new SqlParameter("user_id", UserId);
            SqlParameter productId = new SqlParameter("product_id", product.ProductModel.Id);
            SqlParameter count = new SqlParameter("count", product.Count);
            if (product.Count <= 0)
            {
                BaseProvider.DbContext.Baskets.Remove(BaseProvider.DbContext.Baskets.Single(basketProduct => (basketProduct.UserId == UserId)
                                                                                        && (basketProduct.ProductId == product.ProductModel.Id)));
                Products.Remove(product);
            }
            else
                BaseProvider.CallStoredProcedureByName("UpdateProductCountInBasket", uid, productId, count);
            NotifyBasketChange();
        }

        public void RemoveItem(object sender, RoutedEventArgs e)
        {

        }

        private void ToMainPage(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
