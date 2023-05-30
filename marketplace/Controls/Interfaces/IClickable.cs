using System.Windows;

namespace InternetStore.Controls.Interfaces
{
    internal interface IClickable
    {
        public event RoutedEventHandler Click;
        public event RoutedEventHandler DoubleClick;
    }
}
