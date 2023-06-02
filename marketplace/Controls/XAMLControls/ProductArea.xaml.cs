using InternetStore.Controls.Builders;
using InternetStore.ModelDB;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для ProductArea.xaml
    /// </summary>
    public partial class ProductArea : UserControl
    {
        private LoadDecorator LoadDecorator = new LoadDecorator();

        public bool AddingAccept = false;
        public bool EditionAccept = false;
        public RoutedEventHandler ItemClickHandler = null!;
        public RoutedEventHandler ItemDoubleClickHandler = null!;
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

        public void Load()
        {
            LoadDecorator.Load<Grid>(grid, LoadProducts);
        }

        private void LoadProducts()
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

                if (builder.ItemCount == 0)
                {
                    builder.SetImage(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "outOfStock.png"));
                    builder.SetVisibility("HandledButton", Visibility.Collapsed);
                }
                if (ItemClickHandler != null) NotifyChangeClickHandler(ItemClickHandler);
                if (ItemDoubleClickHandler != null) NotifyChangeDoubleClickHandler(ItemDoubleClickHandler);
                ItemList.Add(builder.Build());
            }
            ListBox.ItemsSource = ItemList;
        }

        public void CreateAdditionalCard()
        {
            Product model = new();
            model.ProductName = "Добавить товар";
            ItemBuilder CardBuilder = new(model, false);
            CardBuilder.SetImage(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "additem.png"));
            CardBuilder.SetFontSize("DescriptionText", 16).SetFontWidth("DescriptionText", FontWeights.Bold);
            CardBuilder.SetVisibility("HandledButton", Visibility.Collapsed);
            CardBuilder.SetVisibility("CostText", Visibility.Collapsed);

            ItemList.Add(CardBuilder.Build());
        }

        private void SortParam(Func<AbsProductView, bool> x = null!)
        {
            if (x != null) ItemList = ItemList
                                        .Where(x)
                                        .Union(ItemList.Where(item => !item.isSortable))
                                        .ToList();

            ItemList.Sort((previousProduct, currentProduct) => -currentProduct.Cost.CompareTo(previousProduct.Cost));
            ListBox.ItemsSource = ItemList;
        }

        public void Sort(string searchString)
        {
            LoadDecorator.Load<Grid>(grid, SearchByName, searchString);
        }

        public void Sort(int minCost, int maxCost)
        {
            LoadDecorator.Load<Grid>(grid, SortByCost, minCost, maxCost);
        }

        public void Sort(int SubCategoryID)
        {
            LoadDecorator.Load<Grid>(grid, SelectSubCategory, SubCategoryID);
        }


        private void SearchByName(string searchText)
        {
            Load();

            if (!searchText.IsNullOrEmpty())
                SortParam(
                    product => product.ItemName
                        .Split(" ")
                        .Select(x => x.Trim().ToLower())
                        .Contains(searchText.ToLower())
                    );
        }

        private void SortByCost(int minCost, int maxCost)
        {
            SortParam(product => Enumerable.Range(minCost, maxCost).Contains((int)(product.Cost)));
        }

        private void SelectSubCategory(int SubCategoryID)
        {
            Load();
            SortParam(product => product.ProductModel.SubcategoryId == SubCategoryID);
        }

        public void NotifyChangeClickHandler(RoutedEventHandler handler)
        {
            ItemClickHandler = handler;
            foreach (var item in ItemList)
            {
                item.UpdateClickHandler(handler);
            }
        }

        public void NotifyChangeDoubleClickHandler(RoutedEventHandler handler)
        {
            ItemDoubleClickHandler = handler;
            foreach (var item in ItemList)
            {
                item.UpdateDoubleClickHandler(handler);
            }
        }
    }
}
