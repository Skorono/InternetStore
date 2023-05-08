using System;
using System.Collections.Generic;

namespace InternetStore.ModelDB;

public partial class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Count { get; set; }

    public virtual Category? Category { get; set; }
}
