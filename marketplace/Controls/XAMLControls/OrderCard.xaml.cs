using DocumentFormat.OpenXml.Office.CustomUI;
using InternetStore.ModelDB;
using InternetStore.Pages;
using System.Data.Entity.Core.Metadata.Edm;
using System.Windows.Media;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для OrderCard.xaml
    /// </summary>
    public partial class OrderCard : ViewControl
    {
        private Order OrderModel = null!;

        #region [ Binding Fields ]
        #endregion

        #region [ Binding Properties ]
        #endregion

        public OrderCard(Order model)
        {
            OrderModel = model;
            this.DoubleClick += ToOrderPage;
            InitializeComponent();
        }

        private void CreateStatusButton()
        {
            if (OrderModel.Paid == true) {
                StatusButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF919492")!;
                StatusButton.Content = "Оплачен";
            }
            else {
                StatusButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF25C35A")!;
                StatusButton.Content = "Не оплачен";
                StatusButton.Click += Payment;
            }
        }

        private void ToOrderPage(object sender, System.Windows.RoutedEventArgs e)
        {
            //NavigationService.GetNavigationService().Navigate(new OrderPage(OrderModel.Id));
        }

        private void Payment(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
