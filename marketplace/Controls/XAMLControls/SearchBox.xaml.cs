using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

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
            SearchBtnImage.Source = new BitmapImage(new Uri(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "searchIcon.png"), UriKind.Relative));
        }

        public void SetSearchHandler(RoutedEventHandler handler)
        {
            this.Click += handler;
        }
    }
}
