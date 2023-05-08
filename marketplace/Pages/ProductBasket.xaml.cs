using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using InternetStore.Controls;
using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductBasket.xaml
    /// </summary>
    public partial class ProductBasket : Page
    {
        private List<IProductView> Products = new List<IProductView>();

        public ProductBasket()
        {
            InitializeComponent();
        }

        private void NotifyPropertyChange()
        {
            if (Products.Count > 0)
                BasketList.ItemsSource = Products;
        }

        public void Add(IProductView product)
        {
            Products.Add(product);
            NotifyPropertyChange();
        }

        public void Remove(IProductView product)
        {
            Products.Remove(product);
            NotifyPropertyChange();
        }
    }
}
