using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace InternetStore.Controls
{
    class UIHelper
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

        public static FieldInfo? FindField<T>(string fieldName) where T : class
        {
            var field = typeof(T).GetField(fieldName, BindingFlags.Public |
                                                      BindingFlags.NonPublic |
                                                      BindingFlags.Instance
                                                      );
            if (field != null)
            {
                return field;
            }
            return null;
        }


    }
}
