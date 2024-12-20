using MilGlorian.Application.DTOs.Auth;

namespace MilGlorian.Application.Abstract.Services;

public interface IAuthService
{
    Task Register(RegisterDTO registerDTO);
}
