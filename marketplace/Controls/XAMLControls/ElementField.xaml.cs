using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    [ContentProperty("Children")]
    public partial class ElementField : UserControl
    {
        static private DependencyProperty? ChildProperty;
        //ChildControl composition = new ChildControl();

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
            get => (UIElement)GetValue(ChildProperty);
            set => SetValue(ChildProperty, value);
        }
    }
}
