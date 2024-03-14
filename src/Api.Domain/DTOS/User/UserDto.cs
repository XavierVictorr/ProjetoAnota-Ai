using System.Globalization;

namespace Domain.DTOS.User;

public class UserDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreateAt { get; set; }
}