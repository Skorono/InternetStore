namespace InternetStore.Controls.XAMLControls
{
    /// <summary>
    /// Логика взаимодействия для TextBlockButton.xaml
    /// </summary>
    public partial class TextBlockButton : ViewControl
    {
        public string Text
        {
            get => InnerBlock.Text;
            set => InnerBlock.Text = value;
        }

        public TextBlockButton()
        {
            InitializeComponent();
        }
    }
}
