using InternetStore.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace InternetStore.Pages
{

    public partial class ProductPage : Page
    {
        private bool EditAccess = false;
        private AbsProductView Product = null!;

        public ProductPage(AbsProductView product, [Optional] bool EditPermission)
        {
            InitializeComponent();
            EditAccess = EditPermission;
            Product = product;
            if (Product.ProductModel.Image != null)
                ProductImage.Source = ImageManager.LoadImage(product.ProductModel.Image!);
            else
                ProductImage.Source = ImageManager.LoadImage(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "emptyProduct.png"));
            if (EditPermission)
            {
                ProductPhoto.Visibility = Visibility.Visible;
                foreach (var editButton in UIHelper.FindAllUid(descriptionGrid, "EditButton")!)
                    ((Button)editButton).Visibility = Visibility.Visible;
            }
            
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

        private DockPanel CreateProperty(string key, object value)
        {
            DockPanel PropertyLine = new();
            TextBlock keyBox = new();
            TextBox valueBox = new();

            PropertyLine.HorizontalAlignment = HorizontalAlignment.Stretch;

            keyBox.Text = $"{key}: ";
            valueBox.Text = value.ToString();

            keyBox.FontWeight = FontWeights.Bold;

            valueBox.HorizontalAlignment = HorizontalAlignment.Right;

            if (!EditAccess)
            {
                valueBox.IsReadOnly = true;
                valueBox.IsReadOnlyCaretVisible = false;
            }
            valueBox.TextChanged += PropertyChanged;
            
            DockPanel.SetDock(keyBox, Dock.Left);
            DockPanel.SetDock(valueBox, Dock.Right);

            PropertyLine.Children.Add(keyBox);
            PropertyLine.Children.Add(valueBox);

            return PropertyLine;
        }

        private void ToMainPage(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public void PropertyChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            DockPanel? DockPanel = VisualTreeHelper.GetParent((TextBox)sender) as DockPanel;
            if (DockPanel != null)
            {
                foreach (var child in DockPanel.Children)
                {
                    if (child.GetType().IsAssignableTo(typeof(TextBlock)))
                    {
                        Product.SetProperty((string.Join("", 
                                            from c in ((TextBlock)child).Text.Trim()
                                                where char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)
                                            select c
                                            )!),
                            ((TextBox)sender).Text);
                    }
                }
            }
        }

        private void ChangeImage(object sender, System.Windows.RoutedEventArgs e)
        {
            Product.Image = ImageManager.ImageSourceToBytes(new TiffBitmapEncoder(), ImageManager.LoadImageFromFileDialog());
            ProductImage.Source = ImageManager.LoadImage(Product.ProductModel.Image);
        }
    }
}