using Client.Web.Model;
using Client.Web.Model.DTO;
using System.Security.Claims;

namespace Client.Web.IServices
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task<string> GetAccessTokenAsync();
        Task<string> GetRefreshTokenAsync();
        Task<IEnumerable<Claim>> ParseClaimsFromJwt(string token);
        Task<bool> Register(UserCreateDTO userCreateDTO);
    }
}
