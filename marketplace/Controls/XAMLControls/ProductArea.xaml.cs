using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;


namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для ProductArea.xaml
    /// </summary>
    public partial class ProductArea : UserControl
    {
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

            if (EditionAccept)
            {
                CreateAdditionalCard();
            }

            foreach (var product in BaseProvider.DbContext.Products.ToList())
            {
                AbsProductView newItem = new Item(product);
                
                if (ItemHandler != null) newItem.Click += ItemHandler;
                ItemList.Add(newItem);
            }
            ListBox.ItemsSource = ItemList;
        }

        public void CreateAdditionalCard()
        {
            Product model = new();
            model.ProductName = "Добавить товар";
            Item additonalCard = new Item(model);
            additonalCard.Image = ImageManager.Upload(@"C:\Users\astat\source\repos\InternetStore\marketplace\Assets\Images\additem.png");
            additonalCard.HandledButton.Visibility = Visibility.Collapsed;
            additonalCard.CostText.Visibility = Visibility.Collapsed;
            additonalCard.DescriptionText.FontSize = 16;
            additonalCard.DescriptionText.FontWeight = FontWeights.Bold;
            ItemList.Add(additonalCard);
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
                ItemHandler = handler;
                item.UpdateHandler(handler);
            }
        }
    }
}
