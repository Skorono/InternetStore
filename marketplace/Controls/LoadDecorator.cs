using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace InternetStore.Controls
{
    internal class LoadDecorator
    {
        public LoadDecorator() { }

        private void LoadGif<T>(T layout) where T : Grid
        {

        }

        [STAThread]
        async public void Load<T>(T layout, Delegate Func, params object[] args) where T : Grid
        {
            //Thread.Sleep(5000);
       
            Image img = new();
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(Path.Combine(Environment.GetEnvironmentVariable("Images")!, "loading3.gif"), UriKind.Relative);
            image.EndInit();
            Grid.SetColumn(img, layout.RowDefinitions.Count / 2);
            Grid.SetRow(img, layout.ColumnDefinitions.Count / 2);
            ImageBehavior.SetAnimatedSource(img, image);
            layout.Children.Add(img);

            await Task.Delay(1200);
            Func.DynamicInvoke(args);
            layout.Children.Remove(img);
        }

        public static Task StartSTATask(Action func)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    func();
                    tcs.SetResult(null);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        /*  private TResult Foo<TResult>(Delegate f, params object[] args)
          {
              var result = f.DynamicInvoke(args);
              return (TResult)Convert.ChangeType(result, typeof(TResult));
          }*/
    }
}
