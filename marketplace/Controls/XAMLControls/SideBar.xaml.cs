using System.Windows.Controls;
using InternetStore.ModelDB;
using System.Collections.Generic;
using System.Linq;


namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для SideBar.xaml
    /// </summary>
    public partial class SideBar : UserControl
    {
        List<CategoryDetails> categoriesList = new();
        public SideBar()
        {
            InitializeComponent();
            foreach (var category in BaseProvider.DbContext.Categories.ToList())
            {
                CategoryDetails categoryTextBlock = new CategoryDetails(category);
                categoriesList.Add(categoryTextBlock);
            }
            CategoryList.ItemsSource = categoriesList;
        }

    }
}
