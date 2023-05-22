using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls.XAMLControls.Icons
{
    /// <summary>
    /// Логика взаимодействия для BasketIcon.xaml
    /// </summary>
    public partial class BasketIcon : UserControl, INotifyPropertyChanged
    {
        #region [ Binding Properties ]
        
        public static DependencyProperty PropertyCount =
            DependencyProperty.Register("Count", typeof(int), typeof(BasketIcon));

        public static readonly RoutedEvent DoubleClickEvent = EventManager.RegisterRoutedEvent("DoubleClick",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BasketIcon));

        #endregion

        #region [ Binding Fields ]
        public virtual int Count
        {
            get
            {
                return (int)GetValue(PropertyCount);
            }

            set
            {
                SetValue(PropertyCount, value);
                NotifyPropertyChanged("Count");
            }
        }

        public event RoutedEventHandler DoubleClick
        {
            add
            {
                base.AddHandler(DoubleClickEvent, value);
            }
            remove
            {
                base.RemoveHandler(DoubleClickEvent, value);
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public BasketIcon()
        {
            InitializeComponent();
        }

        private void Navigate(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(DoubleClickEvent);
            RaiseEvent(args);
        }
    }
}
