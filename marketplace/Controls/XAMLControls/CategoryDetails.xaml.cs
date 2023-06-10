using InternetStore.ModelDB;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace InternetStore.Controls.XAMLControls;

/// <summary>
/// Логика взаимодействия для CategoryDetails.xaml
/// </summary>
public partial class CategoryDetails : UserControl
{
    public Category CategoryModel { get; private set; }
    private RoutedEventHandler ClickHandler = null!;
    List<TextBlockButton> subcategoryList = new();
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
            TextBlockButton subCategoryTextBlock = new();
            subCategoryTextBlock.Text = subcategory.Name;
            if (ClickHandler != null)
                subCategoryTextBlock.Click += ClickHandler;
            subcategoryList.Add(subCategoryTextBlock);
        }
    }

    public void UpdateClickHandler(RoutedEventHandler handler)
    {
        foreach (var subCat in subcategoryList)
        {
            subCat.Click -= ClickHandler;
            subCat.Click += handler;
        }
        ClickHandler = handler;
    }
}
