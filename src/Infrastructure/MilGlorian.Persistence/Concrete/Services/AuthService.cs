using Microsoft.AspNetCore.Identity;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.Auth;
using MilGlorian.Domain.Entities;

namespace MilGlorian.Persistence.Concrete.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;

    public AuthService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Register(RegisterDTO registerDTO)
    {
        AppUser appUser = new()
        {
            FullName = registerDTO.FullName,
            UserName = registerDTO.UserName,
            Email = registerDTO.Email,
            IsActive = true
        };

        var identityResult = await _userManager.CreateAsync(appUser, registerDTO.Password);


    }
}
