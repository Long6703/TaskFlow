using Client.Web.Model;
using System.Security.Claims;

namespace Client.Web.IServices
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task<string> GetTokenAsync();
        Task<IEnumerable<Claim>> ParseClaimsFromJwt(string token);
    }
}
