using InternetStore.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Pages
{

    public partial class ProductPage : Page
    {
        private AbsProductView Product = null!;

        public ProductPage(AbsProductView product)
        {
            InitializeComponent();
            Product = product;
            if (Product.Image != null)
                ProductImage.Source = ImageManager.LoadImage(product.Image);
            else
                ProductImage.Source = ImageManager.LoadImage(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "emptyProduct.png"));
            ProductName.Text = product.ProductModel.ProductName;
            CategoryName.Text = BaseProvider.DbContext.SubCategories.Single(category => category.Id == product.ProductModel.SubcategoryId).Name;
            ProductCount.Text = product.Count.ToString();
            GenerateProperties();
        }

        private void GenerateProperties()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var SubCategory = BaseProvider.DbContext.SubCategories.Single(subcat => subcat.Id == Product.ProductModel.SubcategoryId);
            Dictionary<string, object> SubCategoryAttributes = SubCategory.Attributes.Parse<string, object>();
            if (SubCategoryAttributes != null && Product.Properties != null)
            {
                var prop = Product.Properties.ToList().Where(dict => SubCategoryAttributes.ContainsKey(dict.Key));
                PropertyList.ItemsSource = prop
                    .Select(dict => CreateProperty(dict.Key, dict.Value));
            }
        }

        private TextBlock CreateProperty(string key, object value)
        {
            TextBlock Property = new TextBlock();
            Property.Inlines.Add($"{key}: ");
            Property.Inlines.Add($"{value}");

            Property.Inlines.FirstInline.FontWeight = FontWeights.Bold;

            return Property;
        }

        private void ToMainPage(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}