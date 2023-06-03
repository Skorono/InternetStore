using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            SearchBtnImage.Source = ImageManager.LoadImage(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "searchIcon2.png"));
        }

        public void SetSearchHandler(RoutedEventHandler handler)
        {
            SearchBtn.Click += handler;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}
