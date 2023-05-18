﻿using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InternetStore.Controls
{
    public class AbsBasketProductView: AbsProductView, IBasketViewItem
    {
        #region [ Binding Fields ]

        public static DependencyProperty PropertyCount =
            DependencyProperty.Register("Count", typeof(int), typeof(AbsBasketProductView));

        #endregion

        #region [ Binding Properties ]

        public virtual int Count
        {
            get
            {
                return (int)GetValue(PropertyCount);
            }

            set
            {
                SetValue(PropertyCount, value);
            }
        }

        #endregion

        public AbsBasketProductView(Product model) : base(model) {
            Count = 0;
        }
    }
}