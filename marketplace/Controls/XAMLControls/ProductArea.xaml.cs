﻿using InternetStore.Controls.Builders;
using InternetStore.ModelDB;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для ProductArea.xaml
    /// </summary>
    public partial class ProductArea : UserControl
    {
        public bool AddingAccept = false;
        public bool EditionAccept = false;
        public RoutedEventHandler ItemHandler = null!;
        public List<AbsProductView> ItemList = new();

        public ProductArea()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// 

        public void LoadProducts()
        {
            ItemList.Clear();

            if (AddingAccept)
            {
                CreateAdditionalCard();
            }

            foreach (var product in BaseProvider.DbContext.Products.ToList())
            {
                ItemBuilder builder = new(product);
                builder.isChangeable().isSortable();

                if (EditionAccept)
                {
                    builder.isEdittable();
                }

                if (ItemHandler != null) NotifyChangeHandler(ItemHandler);
                ItemList.Add(builder.Build());
            }
            ListBox.ItemsSource = ItemList;
        }

        public void CreateAdditionalCard()
        {
            Product model = new();
            model.ProductName = "Добавить товар";
            ItemBuilder CardBuilder = new(model);
            CardBuilder.SetImage(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "additem.png"));
            CardBuilder.SetFontSize("DescriptionText", 16).SetFontWidth("DescriptionText", FontWeights.Bold);
            CardBuilder.SetVisibility("HandledButton", Visibility.Collapsed);
            CardBuilder.SetVisibility("CostText", Visibility.Collapsed);

            ItemList.Add(CardBuilder.Build());
        }

        public void Sort(Func<AbsProductView, bool> x = null!)
        { 
            if (x != null) ItemList = ItemList
                                        .Where(x)
                                        .Union(ItemList.Where(item => !item.isSortable))
                                        .ToList();

            ItemList.Sort((previousProduct, currentProduct) => -currentProduct.Cost.CompareTo(previousProduct.Cost));
            ListBox.ItemsSource = ItemList;
        }

        public void SearchByName(string searchText)
        {
            LoadProducts();
            Sort(
                product => product.ItemName
                    .Split(" ")
                    .Select(x => x.Trim().ToLower())
                    .Contains(searchText.ToLower())
                );
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
            ItemHandler = handler;
            foreach (var item in ItemList)
            {
                item.UpdateHandler(handler);
            }
        }
    }
}
