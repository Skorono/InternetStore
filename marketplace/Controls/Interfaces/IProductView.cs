using InternetStore.ModelBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.Controls.Interfaces
{
    interface IProductView
    {
        Product ProductModel { get; }
        byte[] Image { get; }
        string Name { get; }
        float Cost { get; }
    }
}
