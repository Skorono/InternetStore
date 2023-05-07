using InternetStore.Controls.Interfaces;
using InternetStore.ModelBD;
using System;
using System.Windows;
using System.Windows.Controls;


namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl, IProductView
    {
        public Product ProductModel { get; private set; }

        #region [ Binding Fields ]
        public static DependencyProperty PropertyImage =
            DependencyProperty.Register("Image", typeof(Uri), typeof(Item));
        public static DependencyProperty PropertyName =
            DependencyProperty.Register("itemName", typeof(string), typeof(Item));
        public static DependencyProperty PropertyCost =
            DependencyProperty.Register("Cost", typeof(float), typeof(Item));
        #endregion

        #region [ Binding Properties ]
        public byte[] Image { 
            get {
                return (byte[])GetValue(PropertyImage);
            }

            set { 
                SetValue(PropertyImage, value);
            }
        }

        public string? ItemName {
            get
            {
                return (string)GetValue(PropertyName);
            }
            set
            {
                SetValue(PropertyName, value);
            }
        }

        public float Cost {
            get
            {
                return (float)GetValue(PropertyCost);
            }

            set
            {
                SetValue(PropertyCost, value);
            } 
        }
        #endregion

        public Item(Product model)
        {
            ProductModel = model;
            ItemName = ProductModel.ProductName;
            /*PropertyImage = ItemModel.*/
            Cost = ProductModel.Count;
            InitializeComponent();
        }
    }
}
