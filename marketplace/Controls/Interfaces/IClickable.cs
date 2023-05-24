using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InternetStore.Controls.Interfaces
{
    internal interface IClickable
    {
        public event RoutedEventHandler Click;
        public event RoutedEventHandler DoubleClick;
    }
}
