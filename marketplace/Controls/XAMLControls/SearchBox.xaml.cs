using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для SearchBox.xaml
    /// </summary>
    public partial class SearchBox : ViewControl
    {
        public SearchBox()
        {
            InitializeComponent();
        }

        public void SetSearchHandler(RoutedEventHandler handler)
        {
            this.Click += handler;
        }
    }
}
