using InternetStore.ModelDB;



namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : AbsProductView
    {
        public Item(Product model) : base(model)
        {
            InitializeComponent();
        }
    }
}
