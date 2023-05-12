using System.Windows;
using InternetStore.Controls;
using Microsoft.EntityFrameworkCore;

namespace marketplace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() {
            BaseControl.DbContext.Database.ExecuteSqlRaw("SELECT * FROM [User]");
        }
    }
}
