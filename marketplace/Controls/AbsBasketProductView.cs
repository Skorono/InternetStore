using Microsoft.Data.SqlClient;
using InternetStore.Controls.Interfaces;
using InternetStore.ModelDB;
using System.Windows;

namespace InternetStore.Controls
{
    public class AbsBasketProductView : AbsProductView, IBasketViewItem
    {
        public int OwnerId;

        #region [ Binding Fields ]

        public static DependencyProperty PropertyCount =
            DependencyProperty.Register("Count", typeof(int), typeof(AbsBasketProductView));

        #endregion

        #region [ Binding Properties ]

        public virtual int Count
        {
            get => (int)GetValue(PropertyCount);

            set
            {
                SetValue(PropertyCount, value);
                SyncModel();
            }
        }

        #endregion

        public AbsBasketProductView(Product model) : base(model)
        {
            Count = 0;
        }

        protected override void SyncModel()
        {
            if (AllowSync && (OwnerId != 0))
            {
                var user_id = new SqlParameter("user_id", OwnerId);
                var product_id = new SqlParameter("product_id", this.ProductModel.Id);
                var count = new SqlParameter("count", Count);
                BaseProvider.CallStoredProcedureByName("UpdateProductCountInBasket", user_id, product_id, count);
            }
        }
    }
}
