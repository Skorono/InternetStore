using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using System.Collections.Generic;

namespace InternetStore.Controls
{
    public abstract class AbsProductView : UserControl, IProductView, INotifyPropertyChanged
    {
        #region [ Binding Fields ]

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AbsProductView));
        public static DependencyProperty PropertyImage =
            DependencyProperty.Register("Image", typeof(byte[]), typeof(AbsProductView));
        public static DependencyProperty PropertyName =
            DependencyProperty.Register("itemName", typeof(string), typeof(AbsProductView));
        public static DependencyProperty PropertyCost =
            DependencyProperty.Register("Cost", typeof(float), typeof(AbsProductView));
        
        #endregion

        #region [ Binding Properties ]

        public virtual byte[] Image
        {
            get
            {
                return (byte[])GetValue(PropertyImage);
            }

            set
            {
                SetValue(PropertyImage, value);
            }
        }

        public virtual string? ItemName
        {
            get
            {
                return (string)GetValue(PropertyName);
            }
            set
            {
                SetValue(PropertyName, value);
            }
        }

        public virtual float Cost
        {
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

        public event RoutedEventHandler Click
        {
            add
            {
                base.AddHandler(ClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(ClickEvent, value);
            }
        }

        public Product ProductModel { get; protected set; }

        public AbsProductView(Product model)
        {
            ProductModel = model;
            ItemName = ProductModel.ProductName;
            /*PropertyImage = ItemModel.*/
            //var Properties = JsonSerializer.Deserialize<Dictionary<string, string>>(ProductModel.Properties);
            //Cost = float.TryParse(Properties?.cost, out PropertyCost);
        }

        protected virtual void Navigate(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent);
            RaiseEvent(args);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
