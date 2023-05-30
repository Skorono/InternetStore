using InternetStore.ModelDB;

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
    }
}
