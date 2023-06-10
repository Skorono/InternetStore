using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для CategoryList.xaml
    /// </summary>
    public partial class CategoryList : UserControl
    {
        public RoutedEventHandler categoryClickHandler = null!;
        private List<CategoryDetails> categoriesList = new();

        public CategoryList()
        {
            InitializeComponent();
            UpdateCategories();
        }

        public void UpdateCategories()
        {
            foreach (var category in BaseProvider.DbContext.Categories.ToList())
            {
                CategoryDetails categoryTextBlock = new CategoryDetails(category);
                categoriesList.Add(categoryTextBlock);
            }
            CategoryDetailsList.ItemsSource = categoriesList;
        }

        public void UpdateClickHandler(RoutedEventHandler handler)
        {
            categoryClickHandler = handler;
            foreach (var cat in categoriesList) {
                cat.UpdateClickHandler(categoryClickHandler);
            }
        }
    }
}
