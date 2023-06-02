using System.Collections.Generic;
using System.Windows.Controls;

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
