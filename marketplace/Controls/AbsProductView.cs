using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;
using System.Windows;
using System.Collections.Generic;

namespace InternetStore.Controls
{
    public abstract class AbsProductView : ViewControl, IProductView
    {
        #region [ Binding Fields ]

        public static DependencyProperty PropertyImage =
            DependencyProperty.Register("Image", typeof(byte[]), typeof(AbsProductView));
        public static DependencyProperty PropertyName =
            DependencyProperty.Register("itemName", typeof(string), typeof(AbsProductView));
        public static DependencyProperty PropertyCost =
            DependencyProperty.Register("Cost", typeof(float), typeof(AbsProductView));
        
        #endregion

        #region [ Binding Properties ]

        public virtual byte[]? Image
        {
            get => (byte[])GetValue(PropertyImage);

            set => SetValue(PropertyImage, value);
        }

        public virtual string? ItemName
        {
            get => (string)GetValue(PropertyName);
            set => SetValue(PropertyName, value);
        }

        public virtual float Cost
        {
            get => (float)GetValue(PropertyCost);

            set => SetValue(PropertyCost, value);
        }

        #endregion

        public Product ProductModel { get; protected set; }

        public Dictionary<string, object> Properties { get; protected set; }

        public AbsProductView(Product model)
        {
            ProductModel = model;
            ItemName = ProductModel.ProductName;
            ParsePropertiesFromModel();
            Image = (byte[])Properties.GetValue("image"); 
            Cost = float.Parse(Properties.GetValue("cost").ToString());
        }

        private void ParsePropertiesFromModel()
        {
            string jsonString = ProductModel.Properties;
            Properties = jsonString.Parse<string, object>();
        }
    }
}
