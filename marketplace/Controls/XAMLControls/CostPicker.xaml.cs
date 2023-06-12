using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для CostPicker.xaml
    /// </summary>
    public partial class CostPicker : ViewControl
    {
        private static CostPicker _context = null!;
        #region [ Binding Fields ]

        private DependencyProperty MinCostField = DependencyProperty
            .Register("MinCost", typeof(int), typeof(CostPicker));

        private DependencyProperty MaxCostField = DependencyProperty
            .Register("MaxCost", typeof(int), typeof(CostPicker));

        #endregion

        #region [ Binding Properties ]

        public int MinCost
        {
            get => (int)GetValue(MinCostField);

            set
            {
                SetValue(MinCostField, value);
                NotifyPropertyChanged(nameof(MinCost));
            }
        }

        public int MaxCost
        {
            get => (int)GetValue(MaxCostField);

            set
            {
                SetValue(MaxCostField, value);
                NotifyPropertyChanged(nameof(MaxCost));
            }
        }
        #endregion

        private CostPicker()
        {
            InitializeComponent();
            MinCost = 0;
            MaxCost = 1000000;
            CostSlider.Minimum = MinCost;
            CostSlider.Maximum = MaxCost;
        }

        public static CostPicker GetInstance()
        {
            if (_context == null)
                _context = new CostPicker();
            return _context;
        }

        private void OnRangeChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MaxCost = (int)e.NewValue;
        }

        protected override void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            base.NotifyPropertyChanged(propertyName);
            CostSlider.Minimum = MinCost;
            CostSlider.Value = MaxCost;
        }
    }
}
