using InternetStore.Controls.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls
{
    public abstract class ViewControl : UserControl, IClickable, IPropertyChangeable
    {
        #region [ Event Fields ]

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click",
                RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ViewControl));

        public static readonly RoutedEvent DoubleClickEvent = EventManager.RegisterRoutedEvent("DoubleClick",
                RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ViewControl));

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region [ Event Handlers ]
        public event RoutedEventHandler Click
        {
            add => base.AddHandler(ClickEvent, value);
            remove{
                if (value != null)
                    base.RemoveHandler(ClickEvent, value);
            } 
        }

        public event RoutedEventHandler DoubleClick
        {
            add => base.AddHandler(DoubleClickEvent, value);
            remove{
                if (value != null)
                    base.RemoveHandler(DoubleClickEvent, value);
            }
        }

        #endregion

        protected virtual void Clicked(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent);
            RaiseEvent(args);
        }

        protected virtual void DoubleClicked(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(DoubleClickEvent);
            RaiseEvent(args);
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
