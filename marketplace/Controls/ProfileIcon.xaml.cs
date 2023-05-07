using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для ProfileIcon.xaml
    /// </summary>
    public partial class ProfileIcon : UserControl, INotifyPropertyChanged
    {
        
        #region [ Binding Fields ]
        private DependencyProperty usrName =
            DependencyProperty.Register("usrName", typeof(string), typeof(ProfileIcon));

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", 
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ProfileIcon));
        #endregion

        #region [ Binding Properties ]
        public string UserName {
                get{
                    return (string)GetValue(usrName);    
                }

                set{
                    SetValue(usrName, value);
                    NotifyPropertyChanged("UserName");           
                } 
            }

        public event RoutedEventHandler Click
        {
            add {
                base.AddHandler(ClickEvent, value);
            }
            remove { 
                base.RemoveHandler(ClickEvent, value);
            }
        }
        #endregion

        public ProfileIcon()
        {
           InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Navigate(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent);
            RaiseEvent(args);
        }
    }
}
