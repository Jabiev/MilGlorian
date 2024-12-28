using Microsoft.AspNetCore.Mvc;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.Auth;

namespace MilGlorian.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountsController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Connect/[Action]")]
    public async Task<ActionResult> Login(SignInDTO signInDTO)
    {
        var response = await _authService.Login(signInDTO);
        return response.ToActionResult();
    }
    
    [HttpPost("Connect/Token")]
    public async Task<ActionResult> RefreshToken(string refreshToken)
    {
        var response = await _authService.RefreshToken(refreshToken);
        return response.ToActionResult();
    }
    
    [HttpPost("[Action]")]
    public async Task<ActionResult> Register(RegisterDTO registerDTO)
    {
        var response = await _authService.Register(registerDTO);
        return response.ToActionResult();
    }
}
