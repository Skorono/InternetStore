using InternetStore.ModelDB;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Windows.Controls;
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
            CreateStatusButton();
            CreateOrderComposition();
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

        private void CreateOrderComposition()
        {   
            List<TextBlock> ProductsName = new List<TextBlock>();
            List<OrderDetail> OrderDetails = BaseProvider.DbContext.OrderDetails.Where(raw => raw.OrderId == OrderModel.OrderId).ToList();

            foreach (var orderRaw in OrderDetails)
                foreach (var product in BaseProvider.DbContext.Products.Where(p => p.Id == orderRaw.ProductId)) {
                    TextBlock ProductName = new TextBlock();
                    ProductName.Text = product.ProductName;
                    ProductName.TextTrimming = System.Windows.TextTrimming.CharacterEllipsis;
                    ProductsName.Add(ProductName);
                }
            OrderComposition.ItemsSource = ProductsName;
        }

        private void Payment(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
