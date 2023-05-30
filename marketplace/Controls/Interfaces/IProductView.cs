using InternetStore.ModelDB;

namespace InternetStore.Controls.Interfaces
{
    public interface IProductView
    {
        Product ProductModel { get; }
        byte[] Image { get; }
        string Name { get; }
        float Cost { get; }
    }
}
