using System;

namespace InternetStore.ModelDB;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime DatetimeOfForm { get; set; }

    public DateTime? DatetimeOfPayment { get; set; }

    public bool Paid { get; set; }

    public virtual User? User { get; set; }
}
