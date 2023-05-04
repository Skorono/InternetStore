using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using InternetStore.Pages;

namespace InternetStore.Controls
{
    /// <summary>
    /// Логика взаимодействия для ProfileIcon.xaml
    /// </summary>
    public partial class ProfileIcon : UserControl, INotifyPropertyChanged
    {
        private DependencyProperty usrName =
            DependencyProperty.Register("usrName", typeof(string), typeof(ProfileIcon));

        public string UserName {
                get{
                    return (string)GetValue(usrName);    
                }

                set{
                    SetValue(usrName, value);
                    NotifyPropertyChanged("UserName");           
                } 
            }

        public ProfileIcon()
        {
            InitializeComponent();
        }

        private void ProfileNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Profile());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
