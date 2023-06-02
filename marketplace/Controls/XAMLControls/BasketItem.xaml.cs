using InternetStore.ModelDB;
using System.Linq;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для BasketItem.xaml
    /// </summary>
    public partial class BasketItem : AbsBasketProductView
    {
        public BasketItem(Product model) : base(model)
        {
            Count = 1;
            InitializeComponent();
        }

        private void Inc(object sender, System.Windows.RoutedEventArgs e)
        {
            Count++;
        }

        private void Dec(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Count != 1)
            {
                Count--;
            }
        }

    }
}
