using System;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace InternetStore.Controls
{
    internal class LoadDecorator
    {
        public LoadDecorator() { }

        private void LoadGif<T>(T layout) where T : Grid
        {

        }

        public void Load<T>(T layout, Delegate Func, params object[] args) where T : Grid
        {

            Image img = new();
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "loading.gif"), UriKind.Relative);
            image.EndInit();
            Grid.SetColumn(img, layout.RowDefinitions.Count - 1);
            Grid.SetRow(img, layout.ColumnDefinitions.Count - 1);
            ImageBehavior.SetAnimatedSource(img, image);
            layout.Children.Add(img);
            img.UpdateLayout();
            Console.WriteLine(img.ActualHeight);



            Thread.Sleep(500);

            Func.DynamicInvoke(args);

            layout.Children.Remove(img);
            layout.UpdateLayout();
        }

        /*  private TResult Foo<TResult>(Delegate f, params object[] args)
          {
              var result = f.DynamicInvoke(args);
              return (TResult)Convert.ChangeType(result, typeof(TResult));
          }*/
    }
}
