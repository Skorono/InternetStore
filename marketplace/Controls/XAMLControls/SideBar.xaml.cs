using System.Windows.Controls;


namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для SideBar.xaml
    /// </summary>
    public partial class SideBar : UserControl
    {
        private CostPicker costPicker = null!;
        public CostPicker CostPicker { get; set; }
        private static SideBar _context = null!;

        private SideBar()
        {
            InitializeComponent();
            costPicker = CostPicker.GetInstance();
            Grid.SetRow(costPicker, 0);
            Grid.SetColumn(costPicker, 0);
            costPicker.Margin = new System.Windows.Thickness(0, 25, 0, 25);
            grid.Children.Add(costPicker);
        }

        public static SideBar GetInstance()
        {
            if (_context == null)
                _context = new SideBar();
            return _context;
        }
    }
}
