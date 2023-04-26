using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для ChildControl.xaml
    /// </summary>
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

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

}
