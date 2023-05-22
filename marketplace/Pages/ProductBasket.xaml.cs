using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using InternetStore.Controls;
using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductBasket.xaml
    /// </summary>
    public partial class ProductBasket : Page
    {
        private int UserId;
        private List<IBasketViewItem> Products = new List<IBasketViewItem>();

        public int ProductCount { get { return Products.Count; } }

        public ProductBasket(int UID)
        {
            UserId = UID;
            InitializeComponent();
            foreach (var basketProduct in BaseProvider.DbContext.Baskets
                                                    .ToList()
                                                     .Where(userBasket => userBasket.UserId == UserId))
            {
                // Добавить настройку вида продукта в билдере корзины 
                Add(new BasketItem(
                            BaseProvider.DbContext.Products
                            .ToList()
                            .Where(product => product.Id == basketProduct.ProductId)
                            .First()
                        )
                    );
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

            if (inBasket(product.ProductModel.Id)) {
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
            if (product.Count <= 0)
                Products.Remove(product);
            NotifyBasketChange();
        }
    }
}
