using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
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
    }
}
