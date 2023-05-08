using System;
using System.Collections.Generic;

namespace InternetStore.ModelDB;

public partial class UserPersonalInf
{
    public int? Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Surname { get; set; }

    public string? MiddleName { get; set; }

    public byte[]? Photo { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual User? IdNavigation { get; set; }
}
