
namespace Application.Contracts.Identity
{
    public interface ITokenService
    {
        (string AccessToken, string RefreshToken) GenerateTokens(Guid userId, string username, string role, IEnumerable<string> permissions);
        Task<string> RefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}
 