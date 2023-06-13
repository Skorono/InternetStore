using System;
using System.Collections.Generic;

namespace InternetStore.ModelDB;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime DatetimeOfForm { get; set; }

    public DateTime? DatetimeOfPayment { get; set; }

    public int? Cost { get; set; }

    public bool Paid { get; set; }
}
