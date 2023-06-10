using InternetStore.ModelDB;
using InternetStore.Controls.XAMLControls;
using System;
using System.Windows;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace InternetStore.Controls
{
    public abstract class OrderView: ViewControl
    {
        #region [ Binding Fields ]

        private static DependencyProperty TimeField =
            DependencyProperty.Register(
                "DateOfForm", typeof(DateTime), typeof(OrderView)
            );

        #endregion

        #region [ Binding Properties ]

        public DateTime DateOfForm
        {
            get => (DateTime)GetValue(TimeField);
            set => SetValue(TimeField, value);
        }

        #endregion

        protected Order OrderModel = null!;

        public OrderView(Order model)
        {
            OrderModel = model;
        }
    }
}
