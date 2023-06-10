using InternetStore.ModelDB;
using System.Windows.Media;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для OrderCard.xaml
    /// </summary>
    public partial class OrderCard : OrderView
    {
       

        public OrderCard(Order model): base(model)
        {
            this.DoubleClick += ToOrderPage;
            DateOfForm = OrderModel.DatetimeOfForm;
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
