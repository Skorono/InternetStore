namespace InternetStore.ModelDB;

public partial class OrderDetail
{
    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public virtual Order? Order { get; set; }
}
