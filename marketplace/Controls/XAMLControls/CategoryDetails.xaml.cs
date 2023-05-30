﻿using InternetStore.ModelDB;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;


namespace InternetStore.Controls.XAMLControls;

/// <summary>
/// Логика взаимодействия для CategoryDetails.xaml
/// </summary>
public partial class CategoryDetails : UserControl
{
    public Category CategoryModel { get; private set; }

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
            subcategoryList.Add(subCategoryTextBlock);
        }
    }
}
