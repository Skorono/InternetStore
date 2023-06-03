using DocumentFormat.OpenXml.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для Image.xaml
    /// </summary>
    public partial class ImageManager : UserControl
    {
        public ImageManager()
        {
            InitializeComponent();
        }

        public static byte[]? Upload(string path)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(path.ToString());
            ImageConverter imageConverter = new ImageConverter();
            return (byte[]?)imageConverter.ConvertTo(image, typeof(byte[]));
        }

        public static byte[]? Upload<T>(DbSet<T> entity, object identifier) where T : class
        {
            var EntityRows = entity.ToList();
            foreach (var row in EntityRows)
            {
                foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (field.GetValue(row)?.ToString() == identifier.ToString())
                    {
                        return (byte[]?)(row
                                            .GetType()
                                            .GetFields()
                                            .FirstOrDefault(property => (property.GetValue(row)?.GetType() == typeof(byte[])))
                                            ?.GetValue(row));
                    }
                }
            }
            return null;
        }

        public void Save()
        {

        }

        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public static BitmapImage LoadImage(string Path)
        {
            return new BitmapImage(new System.Uri(Path, System.UriKind.Relative));
        }

        public static BitmapImage? LoadImageFromFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                return LoadImage(dialog.FileName);
            }
            return null;
        }

        public static byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        {
            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;

            if (bitmapSource != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }
            return bytes;
        }
    }
}
