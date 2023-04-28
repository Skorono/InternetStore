using System;
using System.Collections.Generic;

namespace InternetStore.ModelBD;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime DatatimeOfForm { get; set; }

    public DateTime? DatatimeOfPayment { get; set; }

    public bool Paid { get; set; }

    public virtual User? User { get; set; }
}
