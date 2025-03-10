using Client.Web.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace Client.Web.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly TokenManagementService _tokenManagementService;
        private readonly ILogger<CustomAuthStateProvider> _logger;
        private readonly ConcurrentDictionary<string, AuthenticationState> _stateCache = new();

        public CustomAuthStateProvider(TokenManagementService tokenManagementService, ILogger<CustomAuthStateProvider> logger)
        {
            _tokenManagementService = tokenManagementService;
            _logger = logger;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenManagementService.GetAccessTokenAsync();
            var cacheKey = token ?? "unauthenticated";

            if (_stateCache.TryGetValue(cacheKey, out var cachedState))
            {
                return cachedState;
            }

            AuthenticationState newState;
            if (string.IsNullOrEmpty(token))
            {
                newState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            else
            {
                try
                {
                    var claims = await ParseClaimsFromJwt(token);
                    var identity = new ClaimsIdentity(claims, "jwt");
                    var user = new ClaimsPrincipal(identity);
                    newState = new AuthenticationState(user);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to parse JWT token");
                    newState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
            }

            _stateCache.TryAdd(cacheKey, newState);
            return newState;
        }

        private async Task<IEnumerable<Claim>> ParseClaimsFromJwt(string token)
        {
            // Giả lập parse JWT (thay bằng logic thực tế)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "user1"),
                new Claim("permissions", "ViewAll")
            };
            return await Task.FromResult(claims);
        }

        public void NotifyUserAuthentication(string token)
        {
            var claims = ParseClaimsFromJwt(token).Result;
            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            var authState = new AuthenticationState(user);

            _stateCache.Clear();
            _stateCache.TryAdd(token, authState);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public void NotifyUserLogout()
        {
            var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            _stateCache.Clear();
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }
    }
}
