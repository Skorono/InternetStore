using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для CategoryList.xaml
    /// </summary>
    public partial class CategoryList : UserControl
    {
        List<CategoryDetails> categoriesList = new();

        public CategoryList()
        {
            InitializeComponent();
            UpdateCategories();
        }

        public void UpdateCategories() {
            foreach (var category in BaseProvider.DbContext.Categories.ToList())
            {
                CategoryDetails categoryTextBlock = new CategoryDetails(category);
                categoriesList.Add(categoryTextBlock);
            }
            CategoryDetailsList.ItemsSource = categoriesList;
        }
    }
}
