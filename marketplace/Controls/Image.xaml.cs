using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
