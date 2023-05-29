using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;


namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для ProductArea.xaml
    /// </summary>
    public partial class ProductArea : UserControl
    {
        private RoutedEventHandler _itemHandler = null!;
        public List<AbsProductView> ItemList = new();

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
                AbsProductView newItem = new Item(product);

                if (_itemHandler != null) newItem.Click += _itemHandler;
                ItemList.Add(newItem);
            }
            ListBox.ItemsSource = ItemList;
        }

        public void Sort(Func<AbsProductView, bool> x = null!)
        {
            if (x != null) ItemList = ItemList.Where(x).ToList();

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
            foreach (var item in ItemList)
            {
                _itemHandler = handler;
                item.UpdateHandler(handler);
            }
        }
    }
}
