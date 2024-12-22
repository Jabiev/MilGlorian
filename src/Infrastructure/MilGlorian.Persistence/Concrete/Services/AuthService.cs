using Microsoft.AspNetCore.Identity;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.Auth;
using MilGlorian.Common.Shared;
using MilGlorian.Domain.Entities;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MilGlorian.Persistence.Concrete.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJWTService _jWTService;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTService jWTService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jWTService = jWTService;
    }

    public async Task<APIResponse<string>> Login(SignInDTO signInDTO)
    {
        var response = new APIResponse<string>();

        var appUser = await _userManager.FindByEmailAsync(signInDTO.UserNameorEmail);
        if (appUser is null)
        {
            appUser = await _userManager.FindByNameAsync(signInDTO.UserNameorEmail);
            if (appUser is null)
            {
                response.Message = "Invalid Login";
                response.ResponseCode = HttpStatusCode.BadRequest;
                return response;
            }
        }

        SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, signInDTO.Password, true);

        if (!signInResult.Succeeded)
        {
            response.Message = "User can't find";
            response.ResponseCode = HttpStatusCode.NotFound;
            return response;
        }
        if (!appUser.IsActive)
        {
            response.Message = "The user isn't active anymore";
            response.ResponseCode = HttpStatusCode.Locked;
            return response;
        }

        List<Claim> claims = new()
        {
            new(ClaimTypes.NameIdentifier, appUser.Id),
            new(ClaimTypes.Name, appUser.UserName)
        };

        foreach (var role in await _userManager.GetRolesAsync(appUser))
            claims.Add(new(ClaimTypes.Role, role));

        response.Payload = _jWTService.GenerateAccessToken(claims);
        response.ResponseCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse<object?>> Register(RegisterDTO registerDTO)
    {
        var response = new APIResponse<object?>();

        AppUser appUser = new()
        {
            FullName = registerDTO.FullName,
            UserName = registerDTO.UserName,
            Email = registerDTO.Email,
            IsActive = true
        };

        var identityResult = await _userManager.CreateAsync(appUser, registerDTO.Password);

        if (!identityResult.Succeeded)
        {
            StringBuilder stringBuilder = new();
            foreach (var error in identityResult.Errors)
                stringBuilder.AppendLine(error.Description);
            response.ResponseCode = HttpStatusCode.UnprocessableContent;
            response.Message = stringBuilder.ToString();
            return response;
        }
        return response;
    }
}
