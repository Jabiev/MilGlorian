using System.Security.Claims;

namespace MilGlorian.Application.Abstract.Services;

public interface IJWTService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
}
