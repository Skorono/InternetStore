using System;
using System.Collections.Generic;

namespace InternetStore.ModelBD;

public partial class SubCategory
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Attributes { get; set; }
}
