using System.Linq;
using System.Windows.Controls;
using InternetStore.Controls;
using Microsoft.EntityFrameworkCore;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для StoreMain.xaml
    /// </summary>
    public partial class StoreMain : Page
    {
        public StoreMain()
        {
            InitializeComponent();
            foreach (var item in BaseControl.DbContext.Products.ToList())
            {
                ItemList.Items.Add(new Item(item.ProductName, 365f));
            }
        }
    }
}
