using System.Collections.Generic;
using System.Windows.Controls;
using InternetStore.Controls;
using InternetStore.ModelBD;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductBasket.xaml
    /// </summary>
    public partial class ProductBasket : Page
    {
        private List<Product> Products = new List<Product>();
        public ProductBasket()
        {
            BasketList.ItemsSource = Products;
            InitializeComponent();
        }

        public void Add()
        {
            //Products.Add();
        }

        public void Remove()
        {
            //Products.Remove();
        }
    }
}
