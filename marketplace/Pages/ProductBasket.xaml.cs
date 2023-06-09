using InternetStore.Controls;
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
        private List<BasketItem> Products = new List<BasketItem>();

        public int ProductCount => Products.Count;

        public ProductBasket(int UID)
        {
            UserId = UID;
            InitializeComponent();
            foreach (var basketProduct in BaseProvider.DbContext.Baskets
                                                    .ToList()
                                                    .Where(userBasket => userBasket.UserId == UserId))
            {
                Product model = BaseProvider.DbContext.Products
                            .ToList()
                            .Where(product => product.Id == basketProduct.ProductId)
                            .First();

                BasketItem basketItem = new(model);
                Add(basketItem);
                NotifyBasketChange();
            }
        }

        private void NotifyBasketChange()
        {
            if (Products.Count > 0)
                BasketList.ItemsSource = Products;
            BasketList.Items.Refresh();
        }

        public bool inBasket(int ID)
        {
            return !Products.Where(product => product.ProductModel.Id == ID).IsNullOrEmpty();
        }

        private void CreateBasketEntitry()
        {

        }

        public void Add(BasketItem product)
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
                product.Width = 525;
                product.DeleteBtn.Click += RemoveItem;
                product.OwnerId = UserId;
                product.AllowSync = true;
                SqlParameter AddingDateTime = new SqlParameter("addDate", DateTime.Now);
                BaseProvider.CallStoredProcedureByName("AddProductToBasket", uid, productId, count, AddingDateTime);
                Products.Add(product);
            }
            //BaseControl.DbContext.Baskets.Add();
            NotifyBasketChange();
        }

        public void Remove(BasketItem product)
        {
            product.Count--;
            SqlParameter uid = new SqlParameter("user_id", UserId);
            SqlParameter productId = new SqlParameter("product_id", product.ProductModel.Id);
            SqlParameter count = new SqlParameter("count", product.Count);
            if (product.Count <= 0)
            {
                _DeleteProductFromDB(product.ProductModel.Id);
                Products.Remove(product);
            }
            else
                BaseProvider.CallStoredProcedureByName("UpdateProductCountInBasket", uid, productId, count);
            NotifyBasketChange();
        }

        private void FormOrder(object sender, RoutedEventArgs e)
        {
            List<BasketItem> orderDetailsList = new();
            orderDetailsList.AddRange(Products.Where(product => product.IsSelected.IsEnabled == true));
            
            foreach (var product in orderDetailsList)
            {
                Products.Remove(product);
            }
            NotifyBasketChange();

            var userId = new SqlParameter("user_id", UserId);
            var DatetimeOfForm = new SqlParameter("datetime_of_form", DateTime.Now);

            var orderId = BaseProvider.CallStoredProcedureByName("FormOrder", userId, DatetimeOfForm);

            foreach (var line in orderDetailsList)
            {
                var orderLineId = new SqlParameter("order_id", orderId);
                var productId = new SqlParameter("product_id", line.ProductModel.Id);
                BaseProvider.CallStoredProcedureByName("AddLineInOrderDetails", orderLineId, productId);
            }
            BaseProvider.DbContext.SaveChangesAsync();
        }

        public void RemoveItem(object sender, RoutedEventArgs e)
        {
            BasketItem Item = ((Button)(sender)).DataContext as BasketItem;
            Products.Remove(Item);
            _DeleteProductFromDB(Item.ProductModel.Id);
            NotifyBasketChange();
        }

        private void ToMainPage(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void _DeleteProductFromDB(int productID)
        {
            var user_id = new SqlParameter("user_id", UserId);
            var product_id = new SqlParameter("product_id", productID);
            BaseProvider.CallStoredProcedureByName("DeleteProductFromBasket", user_id, product_id);
        }
    }
}
