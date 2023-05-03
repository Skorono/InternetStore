using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для ChildCompositor.xaml
    /// </summary>
    public partial class ChildCompositor: UserControl
    {
        static private UserControl? T;
        delegate void OnChildChange(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private OnChildChange? Update;

        public ChildCompositor(UserControl obj)
        {
            T = obj;
        }

        public void SetHandler()
        {
            if (Update != null)
            {
                Update = null;
            }
            Update += OnChildChanged;
        }

        public Delegate? Compose()
        {
            return Update.GetInvocationList().First();
        }

        static void OnChildChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UserControl)d).Content = (UIElement)e.NewValue;
        }
    }
}
