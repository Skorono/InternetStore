using InternetStore.Controls;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace InternetStore.Pages;

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
        StringBuilder stringBuilder= new StringBuilder();
        PropertyList.ItemsSource = Product.Properties.ToList().Select((dict) => (new TextBlock()).Text = $"{dict.Key}: {dict.Value}");
    }

    private void ToMainPage(object sender, System.Windows.RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}