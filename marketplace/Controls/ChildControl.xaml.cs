
    /// <summary>
    /// Логика взаимодействия для ChildControl.xaml
    /// </summary>
using System.Windows;
using System.Windows.Controls;
using InternetStore.Controls.XAMLControls;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для ChildControl.xaml
    /// </summary>
    public partial class ChildControl: UserControl
    {
        static private ChildCompositor? Compositor;
        static private DependencyProperty? ChildProperty;

        public ChildControl(UserControl control)
        {
            Compositor = new ChildCompositor(control);
            Compositor.SetHandler();
            SetChildProperty();
        }

        static void SetChildProperty()
        {
            if (ChildProperty == null)
            {
                ChildProperty = DependencyProperty.Register("Children", typeof(UIElement),
                                        typeof(ElementField), new PropertyMetadata(Compositor.Compose()));
            }
        }
    }

}
