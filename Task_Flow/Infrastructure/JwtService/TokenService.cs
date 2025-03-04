using Application.Contracts.Identity;

namespace Infrastructure.JwtService
{
    public class TokenService : ITokenService
    {
        public (string AccessToken, string RefreshToken) GenerateTokens(Guid userId, string username, string role, IEnumerable<string> permissions)
        {
            throw new NotImplementedException();
        }

        public Task<string> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task RevokeRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
