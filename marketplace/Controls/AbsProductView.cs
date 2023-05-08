using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls
{
    public abstract class AbsProductView : UserControl, IProductView
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
        public virtual byte[]? Image
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
            ProductModel = model;
            ItemName = ProductModel.ProductName;
            /*PropertyImage = ItemModel.*/
            Cost = ProductModel.Count;
        }

        protected virtual void Navigate(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent);
            RaiseEvent(args);
        }
    }
}
