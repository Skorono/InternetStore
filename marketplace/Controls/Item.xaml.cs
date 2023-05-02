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

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        public Uri? Image { 
            get {
                return (Uri)GetValue(PropertyImage);
            }

            set { 
                SetValue(PropertyImage, value);
            }
        }

        public string? ItemName {
            get
            {
                return (string)GetValue(PropertyName);
            }
            set
            {
                SetValue(PropertyName, value);
            }
        }

        public float Cost {
            get
            {
                return (float)GetValue(PropertyCost);
            }

            set
            {
                SetValue(PropertyCost, value);
            } 
        }

        public static DependencyProperty PropertyImage =
            DependencyProperty.Register("Image", typeof(Uri), typeof(Item));
        public static DependencyProperty PropertyName =
            DependencyProperty.Register("itemName", typeof(string), typeof(Item));
        public static DependencyProperty PropertyCost =
            DependencyProperty.Register("Cost", typeof(float), typeof(Item));


        public Item(string name, float cost)
        {
            ItemName = name;
            Cost = cost;
            InitializeComponent();
        }
    }
}
