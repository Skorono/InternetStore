using System.Windows;
using System.Windows.Controls;
using InternetStore.Controls.XAMLControls.Icons;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// 
    /// Логика взаимодействия для ToolBox.xaml
    /// </summary>
    public partial class ToolBox : UserControl
    {
        private static ToolBox _context = null!;
        public ProfileIcon ProfileIcon = ProfileIcon.GetInstance();
        private ToolBox()
        {
            InitializeComponent();
            InitProfileIcon();
        }

        public static ToolBox GetInstance()
        {
            if (_context == null)
                _context = new ToolBox();
            return _context;
        }
        
        private void InitProfileIcon()
        {
            grid.Children.Add(ProfileIcon);
            Grid.SetColumn(ProfileIcon, 3);
            Grid.SetRow(ProfileIcon, 0);
        }
    }
}
