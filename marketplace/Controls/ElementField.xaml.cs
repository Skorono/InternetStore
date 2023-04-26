using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    [ContentProperty("Children")]
    public partial class ElementField : UserControl
    {
        static private DependencyProperty? ChildProperty;
        ChildControl composition = new ChildControl();

        public ElementField()
        {
            InitializeComponent();
            if (ChildProperty == null)
            {
                ChildProperty = DependencyProperty.Register("Children", typeof(UIElement),
                                        typeof(ElementField), new PropertyMetadata(OnChildChanged));
            }
        }

        static void OnChildChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ElementField)d).entryField.Child = (UIElement)e.NewValue;
        }

        public UIElement Children
        {
            get { return (UIElement)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }
    }
}
