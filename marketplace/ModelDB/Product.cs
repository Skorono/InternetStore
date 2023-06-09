using System;
using System.Collections.Generic;

namespace InternetStore.ModelDB;

public partial class Product
{
    public int Id { get; set; }

    public int? SubcategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Properties { get; set; } = null!;

    public byte[]? Image { get; set; }

    public virtual SubCategory? Subcategory { get; set; }
}
