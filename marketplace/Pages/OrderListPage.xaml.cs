﻿using DocumentFormat.OpenXml.Drawing.Charts;
using InternetStore.Controls;
using InternetStore.Controls.XAMLControls;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Xceed.Wpf.Toolkit;

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
            ShowOrders();
        }

        public static OrderListPage GetInstance(int UID)
        {
            if (_context == null)
                _context = new OrderListPage(UID);
           // else
           //     ShowOrders();
            return _context;
        }

        private void ShowOrders()
        {
            foreach (var order in BaseProvider.DbContext.Orders.Where(order => order.UserId == UserId))
            {
                Orders.Add(new OrderCard(order));
            }
            OrdersList.ItemsSource = Orders;
            OrdersList.Items.Refresh();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //private void ToOrderPage(Royre)
    }
}
