using System;
using System.Collections.Generic;
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
        }

        public void LoadProducts()
        {
            foreach (var product in BaseProvider.DbContext.Products.ToList())
            {
                Item newItem = new Item(product);
                newItem.Click += AddToBasket;
                ItemList.Add(newItem);
            }
            ListBox.ItemsSource = ItemList;
        }
    }
}
