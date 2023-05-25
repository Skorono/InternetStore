using System;
using System.Windows;
using System.Runtime.CompilerServices;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для CostPicker.xaml
    /// </summary>
    public partial class CostPicker : ViewControl
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
                    NotifyPropertyChanged(nameof(MinCost));
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
                    NotifyPropertyChanged(nameof(MinCost));
                }
        }
        #endregion

        public CostPicker()
        {
            InitializeComponent();
            MinCost = 0;
            MaxCost = 1000000;
        }

        private void OnRangeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MaxCost = (int)e.NewValue;
        }

        protected override void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            base.NotifyPropertyChanged(propertyName);
            CostSlider.Minimum = MinCost;
            CostSlider.Maximum = MaxCost;
            CostSlider.SelectionStart = MinCost;
            CostSlider.SelectionEnd = MaxCost;
        }
    }
}
