using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls.XAMLControls.Icons
{
    /// <summary>
    /// Логика взаимодействия для BasketIcon.xaml
    /// </summary>
    public partial class BasketIcon : ViewControl
    {
        #region [ Binding Properties ]
        
        public static DependencyProperty PropertyCount =
            DependencyProperty.Register("Count", typeof(int), typeof(BasketIcon));

        #endregion

        #region [ Binding Fields ]
        public virtual int Count
        {
            get => (int)GetValue(PropertyCount);

            set
            {
                SetValue(PropertyCount, value);
                NotifyPropertyChanged("Count");
            }
        }

        #endregion

        public BasketIcon()
        {
            InitializeComponent();
        }
    }
}
