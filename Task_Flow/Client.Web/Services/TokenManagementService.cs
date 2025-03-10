using Microsoft.Extensions.Caching.Distributed;

namespace Client.Web.Services
{
    public class TokenManagementService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string AccessTokenKey = "AccessToken";
        private const string RefreshTokenKey = "RefreshToken";

        public TokenManagementService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return null;
            return await Task.FromResult(session.GetString(AccessTokenKey));
        }

        public async Task<string> GetRefreshTokenAsync()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return null;
            return await Task.FromResult(session.GetString(RefreshTokenKey));
        }

        public async Task SetTokensAsync(string accessToken, string refreshToken)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return;
            session.SetString(AccessTokenKey, accessToken);
            session.SetString(RefreshTokenKey, refreshToken);
            await Task.CompletedTask;
        }

        public async Task RemoveTokensAsync()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null) return;
            session.Remove(AccessTokenKey);
            session.Remove(RefreshTokenKey);
            await Task.CompletedTask;
        }


    }
}
