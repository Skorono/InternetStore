using InternetStore.Controls;
using System.Linq;
using System.Windows;

namespace marketplace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            BaseProvider.DbContext.Products.Count();
        }
    }
}
