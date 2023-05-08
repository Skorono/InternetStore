using System;
using System.Collections.Generic;

namespace InternetStore.ModelDB;

public partial class Basket
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime AddDate { get; set; }

    public int Count { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
