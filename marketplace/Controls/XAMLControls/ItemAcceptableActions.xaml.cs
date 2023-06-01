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
    /// Логика взаимодействия для ItemAcceptableActions.xaml
    /// </summary>
    /// 

    public partial class ItemAcceptableActions : UserControl
    {
        public List<TextBlockButton> AcceptableActionList = new();

        public ItemAcceptableActions()
        {
            InitializeComponent();
        }

        public void UpdateActionList()
        {
            if (AcceptableActionList != null) ActionList.ItemsSource = AcceptableActionList;
        }
    }
}
