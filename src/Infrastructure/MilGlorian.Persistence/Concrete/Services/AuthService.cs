using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.Auth;
using MilGlorian.Application.Validators.Auth;
using MilGlorian.Common.Shared;
using MilGlorian.Domain.Entities;
using MilGlorian.Infrastructure.Services.JWT;
using MilGlorian.Persistence.Contexts;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MilGlorian.Persistence.Concrete.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJWTService _jWTService;
    private readonly IConfiguration _configuration;
    private readonly MilGlorianDbContext _milGlorianDbContext;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTService jWTService, IConfiguration configuration, MilGlorianDbContext milGlorianDbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jWTService = jWTService;
        _configuration = configuration;
        _milGlorianDbContext = milGlorianDbContext;
    }

    public async Task<APIResponse<TokenResponse>> Login(SignInDTO signInDTO)
    {
        var response = new APIResponse<TokenResponse>();

        SignInDTOValidator validations = new();

        var result = await validations.ValidateAsync(signInDTO);

        if (!result.IsValid)
        {
            StringBuilder stringBuilder = new();
            foreach (var error in result.Errors)
                stringBuilder.AppendLine(error.ErrorMessage);
            response.ResponseCode = HttpStatusCode.UnprocessableContent;
            response.Message = stringBuilder.ToString();
            return response;
        }

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
            new(ClaimTypes.Name, appUser.UserName),
            new(ClaimTypes.GivenName, appUser.FullName)
        };

        foreach (var role in await _userManager.GetRolesAsync(appUser))
            claims.Add(new(ClaimTypes.Role, role));

        string accessToken = _jWTService.GenerateAccessToken(claims);
        string refreshToken = _jWTService.GenerateRefreshToken();
        _ = int.TryParse(_configuration["JWTSettings:RefreshTokenExpirationMinutes"], out int refreshTokenExpiryTime);
        appUser.RefreshToken = refreshToken;
        appUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenExpiryTime);
        await _userManager.UpdateAsync(appUser);

        response.Payload = new TokenResponse(accessToken, refreshToken, appUser.RefreshTokenExpiryTime);
        response.ResponseCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse<TokenResponse>> RefreshToken(string requestRefreshToken)
    {
        var response = new APIResponse<TokenResponse>();

        var appUser = await _milGlorianDbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == requestRefreshToken);
        if(appUser is null)
        {
            response.ResponseCode = HttpStatusCode.BadRequest;
            response.Message = "The refreshToken isn't valid";
            return response;
        }
        if(appUser.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            response.ResponseCode = HttpStatusCode.BadRequest;
            response.Message = "The expireTime of the refreshToken has expired";
            return response;
        }

        List<Claim> claims = new()
        {
            new(ClaimTypes.NameIdentifier, appUser.Id),
            new(ClaimTypes.Name, appUser.UserName),
            new(ClaimTypes.GivenName, appUser.FullName)
        };

        foreach (var role in await _userManager.GetRolesAsync(appUser))
            claims.Add(new(ClaimTypes.Role, role));

        string accessToken = _jWTService.GenerateAccessToken(claims);
        string refreshToken = _jWTService.GenerateRefreshToken();
        _ = int.TryParse(_configuration["JWTSettings:RefreshTokenExpirationMinutes"], out int refreshTokenExpiryTime);
        appUser.RefreshToken = refreshToken;
        appUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenExpiryTime);
        await _userManager.UpdateAsync(appUser);

        response.Payload = new TokenResponse(accessToken, refreshToken, appUser.RefreshTokenExpiryTime);
        response.ResponseCode = HttpStatusCode.OK;
        return response;

    }

    public async Task<APIResponse<object?>> Register(RegisterDTO registerDTO)
    {
        var response = new APIResponse<object?>();

        RegisterDTOValidator validations = new();

        var result = await validations.ValidateAsync(registerDTO);

        if (!result.IsValid)
        {
            StringBuilder stringBuilder = new();
            foreach (var error in result.Errors)
                stringBuilder.AppendLine(error.ErrorMessage);
            response.ResponseCode = HttpStatusCode.UnprocessableContent;
            response.Message = stringBuilder.ToString();
            return response;
        }

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
