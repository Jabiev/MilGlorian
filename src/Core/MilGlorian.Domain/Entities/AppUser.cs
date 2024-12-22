using Microsoft.AspNetCore.Identity;

namespace MilGlorian.Domain.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
    public bool IsActive { get; set; }
}
