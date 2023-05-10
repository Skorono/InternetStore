using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для BasketIcon.xaml
    /// </summary>
    public partial class BasketIcon : UserControl, INotifyPropertyChanged
    {
        #region [ Binding Properties ]
        public static DependencyProperty PropertyCount =
            DependencyProperty.Register("Count", typeof(int), typeof(BasketIcon));
        #endregion

        #region [ Binding Fields ]
        public virtual int Count
        {
            get
            {
                return (int)GetValue(PropertyCount);
            }

            set
            {
                SetValue(PropertyCount, value);
                NotifyPropertyChanged("Count");
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public BasketIcon()
        {
            InitializeComponent();
        }
    }
}
