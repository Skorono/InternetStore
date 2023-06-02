using System;
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
        public App()
        {
            Environment.SetEnvironmentVariable("Images", "../../../Assets/Images/");
            BaseProvider.DbContext.Database.OpenConnection();
        }
    }
}
