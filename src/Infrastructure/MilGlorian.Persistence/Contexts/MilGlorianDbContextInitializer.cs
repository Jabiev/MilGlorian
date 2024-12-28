using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MilGlorian.Domain;
using MilGlorian.Domain.Entities;

namespace MilGlorian.Persistence.Contexts;

public class MilGlorianDbContextInitializer
{
    private readonly MilGlorianDbContext _milGlorianDbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public MilGlorianDbContextInitializer(MilGlorianDbContext milGlorianDbContext,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _milGlorianDbContext = milGlorianDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task InitializeAsync()
    {
        await _milGlorianDbContext.Database.MigrateAsync();
    }

    public async Task RoleSeedAsync()
    {
        foreach (var role in Enum.GetValues(typeof(Role)))
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
                await _roleManager.CreateAsync(new()
                {
                    Name = role.ToString()
                });
    }

    public async Task UserSeedAsync()
    {
        AppUser appUser = new()
        {
            UserName = _configuration["SuperAdminSettings:UserName"],
            Email = _configuration["SuperAdminSettings:Email"],
            FullName = _configuration["SuperAdminSettings:FullName"]
        };
        appUser.IsActive = true;
        await _userManager.CreateAsync(appUser, _configuration["SuperAdminSettings:Password"]);
        await _userManager.AddToRoleAsync(appUser, Role.SuperAdmin.ToString());
    }
}
