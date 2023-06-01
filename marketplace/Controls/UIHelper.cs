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

        public static T? FindParent<T>(DependencyObject child, string parentName)
        where T : DependencyObject
        {
            if (child == null) return null;

            T? foundParent = null;
            var currentParent = VisualTreeHelper.GetParent(child);

            do
            {
                var frameworkElement = currentParent as FrameworkElement;
                if (frameworkElement.Name == parentName && frameworkElement is T)
                {
                    foundParent = (T)currentParent;
                    break;
                }

                currentParent = VisualTreeHelper.GetParent(currentParent);

            } while (currentParent != null);

            return foundParent;
        }

    }
}
