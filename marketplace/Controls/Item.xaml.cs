﻿using System;
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

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        public BitmapImage? Image { get; set; }
        public string? ItemName { get; set; }
        public float Cost { get; set; }
        public static DependencyProperty ProtocolNumberProperty =
       DependencyProperty.Register("ProtocolNumber", typeof(int), typeof(Item));
        public Item(string name, float cost)
        {
            Name = name;
            Cost = cost;

            InitializeComponent();
        }
    }
}
