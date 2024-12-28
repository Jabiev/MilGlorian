using MilGlorian.Application.DTOs.Auth;
using MilGlorian.Common.Shared;

namespace MilGlorian.Application.Abstract.Services;

public interface IAuthService
{
    Task<APIResponse<object?>> Register(RegisterDTO registerDTO);
    Task<APIResponse<TokenResponse>> Login(SignInDTO signInDTO);
    Task<APIResponse<TokenResponse>> RefreshToken(string requestRefreshToken);
}
