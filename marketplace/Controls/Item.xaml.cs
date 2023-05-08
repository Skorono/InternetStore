using InternetStore.ModelDB;
using System.Windows;



namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : AbsProductView
    {
        public Item(Product model): base(model)
        {
            InitializeComponent();
        }
    }
}
