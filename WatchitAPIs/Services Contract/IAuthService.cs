using System.IdentityModel.Tokens.Jwt;
using WatchitAPIs.Models;

namespace WatchitAPIs.Services_Contract
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateTokenAsync(AppUser user);
    }
}
