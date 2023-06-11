using InternetStore.Controls;
using InternetStore.Controls.XAMLControls;
using InternetStore.ModelDB;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace InternetStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderListPage : Page
    {
        private int UserId;
        private List<OrderCard> Orders = new List<OrderCard>();
        private static OrderListPage _context = null!;


        private OrderListPage(int UID)
        {
            UserId = UID;
            InitializeComponent();
            CreateOrdersCard();
        }

        public static OrderListPage GetInstance(int UID)
        {
            if (_context == null)
                _context = new OrderListPage(UID);
           // else
           //     ShowOrders();
            return _context;
        }

        private void CreateOrdersCard()
        {
            List<Order> orderList = BaseProvider.DbContext.Orders
                .Where(order => order.UserId == UserId)
                .ToList();
            orderList.Reverse();

            foreach (var order in orderList.Where(_order => Orders.Where(product => product.CardOrderModel == _order).IsNullOrEmpty()))
            {
                Orders.Add(new OrderCard(order));
            }
            OrdersList.ItemsSource = Orders;
            OrdersList.Items.Refresh();
        }

        private void  UpdateOrderList(object sender, RoutedEventArgs e)
        {
            CreateOrdersCard();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //private void ToOrderPage(Royre)
    }
}
