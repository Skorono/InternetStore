using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace InternetStore.Controls
{
    static class UIHelper
    {
        public static UIElement? FindUid(DependencyObject parent, string uid)
        {
            UIElement? el = new UIElement();
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                el = VisualTreeHelper.GetChild(parent, i) as UIElement;
                if (el != null)
                {
                    if (el?.Uid == uid)
                        return el;
                    el = FindUid(el, uid);
                    if (el != null) return el;
                }
            }
            return null;
        }
    }
}
