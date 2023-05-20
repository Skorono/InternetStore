using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InternetStore.ModelDB;


namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для CategoryDetails.xaml
    /// </summary>
    public partial class CategoryDetails : UserControl
    {
        public Category CategoryModel { get; private set; }

        List<TextBlock> subcategoryList = new List<TextBlock>();
        public CategoryDetails(Category model)
        {
            InitializeComponent();
            
            CategoryModel = model;
            CategoryDropDown.Header = CategoryModel.CategoryName;
            
            UpdateSubcategory();
            if (subcategoryList != null)
                SubcategoryList.ItemsSource = subcategoryList;
        }

        public void UpdateSubcategory()
        {
            foreach (var subcategory in BaseProvider.DbContext.SubCategories
                                                        .ToList()
                                                        .Where(subCat => subCat.CategoryId == CategoryModel.Id))
            {
                TextBlock subCategoryTextBlock = new TextBlock();
                subCategoryTextBlock.Text = subcategory.Name;
                subcategoryList.Add(subCategoryTextBlock);
            }
        }
    }
}
