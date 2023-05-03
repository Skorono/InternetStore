using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для Image.xaml
    /// </summary>
    public partial class Image : UserControl
    {
        public Image()
        {
            InitializeComponent();
        }

        public void Upload(Uri path)
        {
            BDImage.Source = new BitmapImage(path);
        }

        public void Save()
        {

        }
    }
}
