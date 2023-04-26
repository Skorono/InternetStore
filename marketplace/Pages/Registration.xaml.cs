using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private BitmapImage UsrAvatar {
            get { return UsrAvatar; }
            set { UsrAvatar = value; }
        }

        public Registration()
        {
            string ImgPath = Path.Combine(@"pack://application:,,,/Assets/Images/camera_200.png");
            UsrAvatar = new BitmapImage(new Uri(ImgPath, UriKind.Relative));
            InitializeComponent();
        }

        public void SelectImageCommand(object sender, DragEventArgs e) {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
                UsrAvatar = new BitmapImage(new Uri(dlg.FileName, UriKind.Relative));
        }
    }
}
