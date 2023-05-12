using InternetStore.ModelBD;
using System;
using System.Collections.Generic;

namespace InternetStore.ModelDB;

public partial class Product
{
    public int Id { get; set; }

    public int? SubCategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Properties { get; set; } = null!;

    public virtual SubCategory? SubCategory { get; set; }
}
