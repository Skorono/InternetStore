namespace InternetStore.ModelDB;

public partial class UserViewDto
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string? Surname { get; set; }

    public string? MiddleName { get; set; }

    public byte[]? Photo { get; set; }

    public string? PhoneNumber { get; set; }
}
