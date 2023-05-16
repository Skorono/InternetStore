using System;
using System.Collections.Generic;

namespace InternetStore.ModelDB;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
