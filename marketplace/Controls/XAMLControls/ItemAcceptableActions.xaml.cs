using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для ItemAcceptableActions.xaml
    /// </summary>
    /// 

    public partial class ItemAcceptableActions : UserControl
    {
        public bool DeletePermissions = false;
        public List<TextBlockButton> AcceptableActionList = new();

        public ItemAcceptableActions()
        {
            InitializeComponent();
        }

        public void UpdateActionList()
        {

            if (DeletePermissions) {
                TextBlockButton deleteButton = new TextBlockButton();
                deleteButton.Text = "Удалить товар";
                deleteButton.Foreground = Brushes.Red;
                //deleteButton.Click = ;
                AcceptableActionList.Add(deleteButton);
            }
            ActionList.ItemsSource = AcceptableActionList;
        }
    }
}
