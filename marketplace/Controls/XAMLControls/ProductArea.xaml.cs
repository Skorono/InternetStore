using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public void LoadProducts()
        {
            foreach (var product in BaseProvider.DbContext.Products.ToList())
            {
                Item newItem = new Item(product);
                //newItem.Click += AddToBasket;
                ItemList.Add(newItem);
            }
            ListBox.ItemsSource = ItemList;
        }

        public void SortByCost(int minCost, int maxCost)
        {
            foreach (var item in ItemList)
            {
                if (!(Enumerable.Range(minCost, maxCost).Contains((int)item.Cost)))
                    item.Visibility = Visibility.Collapsed;
            }
        }

        public void SortBySubCategory(string name)
        {
            foreach (var item in ItemList)
            {
                if (BaseProvider.DbContext.SubCategories.Single(subcat => subcat.Id == item.ProductModel.SubcategoryId)?.Name.Normalize() != name.Normalize())
                    item.Visibility = Visibility.Collapsed;
            }
        }

        public void ChangeVisibility()
        {

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
