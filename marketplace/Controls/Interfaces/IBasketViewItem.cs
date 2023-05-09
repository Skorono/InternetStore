using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.Controls.Interfaces
{
    public interface IBasketViewItem: IProductView
    {
        int Count { get; set; }

    }
}
