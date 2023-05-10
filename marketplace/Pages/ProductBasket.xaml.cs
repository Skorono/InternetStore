using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using InternetStore.Controls;
using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;
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

        public void Add(IBasketViewItem product)
        {
            if (inBasket(product.ProductModel.Id))
                Products.Where(Bproduct => Bproduct.ProductModel.Id == product.ProductModel.Id).First()
                    .Count++;
            else
                Products.Add(product);
            //BaseControl.DbContext.Baskets.Add();
            NotifyBasketChange();
        }

        private void CreateBasketEntitry()
        {

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
