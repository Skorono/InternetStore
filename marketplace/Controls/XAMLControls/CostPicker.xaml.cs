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

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для CostPicker.xaml
    /// </summary>
    public partial class CostPicker : UserControl
    {
        #region [ Binding Fields ]

            private DependencyProperty MinCostField = DependencyProperty
                .Register("MinCost", typeof(int), typeof(CostPicker));

            private DependencyProperty MaxCostField = DependencyProperty
                .Register("MaxCost", typeof(int), typeof(CostPicker));

        #endregion

        #region [ Binding Properties ]

            public int MinCost
            {
                get
                {
                  return (int)GetValue(MinCostField);
                }
            
                set
                {
                    SetValue(MinCostField, value);
                }
            }

            public int MaxCost
            {
                get
                {
                    return (int)GetValue(MaxCostField);
                }

                set
                {
                    SetValue(MaxCostField, value);
                }
            }
        #endregion

        public CostPicker()
        {
            InitializeComponent();
            MinCost = 0;
            MaxCost = 0;
            CostSlider.Minimum = MinCost;
            CostSlider.Maximum = MaxCost;
        }

        private void OnRangeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MaxCost = (int)e.NewValue;
        }
    }
}
