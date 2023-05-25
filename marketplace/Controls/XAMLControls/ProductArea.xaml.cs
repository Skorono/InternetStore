using System;
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
        public List<Item> ItemList = new List<Item>();

        public ProductArea()
        {
            InitializeComponent();
            LoadProducts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        public void LoadProducts(Func<Product, bool> x = null!)
        {
            ItemList.Clear();
            var Products = BaseProvider.DbContext.Products.ToList();
            if (x != null) Products = Products.Where(x).ToList();

            foreach (var product in Products)
            {
                Item newItem = new Item(product);
                ItemList.Add(newItem);
            }
            ListBox.ItemsSource = ItemList;
        }

        public void SortByCost(int minCost, int maxCost)
        {
            LoadProducts(product => Enumerable.Range(minCost, maxCost).Contains((int)(new Item(product).Cost)));
        }

        public void SortBySubCategory(int SubCategoryID)
        {
            LoadProducts(product => product.SubcategoryId == SubCategoryID);
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
