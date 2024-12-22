using MilGlorian.Application.DTOs.Auth;
using MilGlorian.Common.Shared;

namespace MilGlorian.Application.Abstract.Services;

public interface IAuthService
{
    Task<APIResponse<object?>> Register(RegisterDTO registerDTO);
    Task<APIResponse<string>> Login(SignInDTO signInDTO);
}
