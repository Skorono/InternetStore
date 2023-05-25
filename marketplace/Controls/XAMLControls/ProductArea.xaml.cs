﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InternetStore.ModelDB;


namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для ProductArea.xaml
    /// </summary>
    public partial class ProductArea : UserControl
    {
        public List<Item> ItemList = new();

        public ProductArea()
        {
            InitializeComponent();
            LoadProducts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        public void LoadProducts()
        {
            ItemList.Clear();
            foreach (var product in BaseProvider.DbContext.Products.ToList())
            {
                Item newItem = new Item(product);
                ItemList.Add(newItem);
            }
            ListBox.ItemsSource = ItemList;
        }

        public void Sort(Func<Item, bool> x = null!)
        {
            if (x != null) ItemList = ItemList.Where(x).ToList();

            ItemList.Sort((previousProduct, currentProduct) => -previousProduct.Cost.CompareTo(currentProduct.Cost));
            ListBox.ItemsSource = ItemList;
        }

        public void SearchByName(string searchText)
        {
            Sort(product => product.Name.Contains(searchText));
        }

        public void SortByCost(int minCost, int maxCost)
        {
            Sort(product => Enumerable.Range(minCost, maxCost).Contains((int)(product.Cost)));
        }

        public void SelectSubCategory(int SubCategoryID)
        {
            LoadProducts();
            Sort(product => product.ProductModel.SubcategoryId == SubCategoryID);
        }

        public void NotifyChangeHandler(RoutedEventHandler handler)
        {
            foreach (var item in ItemList)
            {
                item.UpdateHandler(handler);
            }
        }
    }
}
