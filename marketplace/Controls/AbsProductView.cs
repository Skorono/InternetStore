﻿using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Text.Json;
using System.CodeDom;

namespace InternetStore.Controls
{
    public abstract class AbsProductView : ViewControl, IProductView
    {
        public bool AllowSync = false;
        private RoutedEventHandler _itemClickHandler = null!;
        private RoutedEventHandler _itemDoubleClickHandler = null!;

        private int count;

        public int Count
        {
            get => count;

            set
            {
                count = value;
                SyncModel();
            }
        }

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

            set
            {
                SetValue(PropertyImage, value);
                SyncModel();
            }
        }

        public virtual string? ItemName
        {
            get => (string)GetValue(PropertyName);
            set
            {
                SetValue(PropertyName, value);
                SyncModel();
            }
        }

        public virtual float Cost
        {
            get => (float)GetValue(PropertyCost);

            set
            {
                SetValue(PropertyCost, value);
                SyncModel();
            }
        }

        #endregion

        private Dictionary<string, object> properties;

        public Product ProductModel { get; protected set; }
        public Dictionary<string, object> Properties { get; protected set; }

        public bool isSortable = false;
        public bool isChangeable = false;

        public AbsProductView(Product model)
        {
            ProductModel = model;
            ItemName = ProductModel.ProductName;
            ParsePropertiesFromModel();
            float cost = 0.0f;
            int count = 0;
            float.TryParse(Properties.GetValue("cost")?.ToString(), out cost);
            int.TryParse(Properties.GetValue("count")?.ToString(), out count);
            object? imageData = Properties.GetValue("image");
            if (imageData != null)
            {
                byte[]? image = Encoding.ASCII.GetBytes(imageData.ToString());
                Image = image;
            }
            else
                Image = null;
            Cost = cost;
            Count = count;
        }

        private void ParsePropertiesFromModel()
        {
            string jsonString = ProductModel.Properties;
            Properties = jsonString.Parse<string, object>();
        }

        public virtual void UpdateClickHandler(RoutedEventHandler handler)
        {
            this.Click -= _itemClickHandler;
            this.Click += handler;
            _itemClickHandler = handler;
        }

        public virtual void UpdateDoubleClickHandler(RoutedEventHandler handler)
        {
            this.DoubleClick -= _itemDoubleClickHandler;
            this.DoubleClick += handler;
            _itemDoubleClickHandler = handler;
        }

        public virtual void SetProperty(string key, object value)
        {
            if (Properties.ContainsKey(key))
            {
                Properties[key] = value;
                SyncModel();
            }
        }

        private void SyncModel()
        {
            if (AllowSync)
            {
                if (Properties != null)
                    ProductModel.Properties = JsonSerializer.Serialize<Dictionary<string, object>>(Properties);
                BaseProvider.DbContext.Products.Update(ProductModel);
                BaseProvider.DbContext.SaveChangesAsync();
            }
        }
    }
}
