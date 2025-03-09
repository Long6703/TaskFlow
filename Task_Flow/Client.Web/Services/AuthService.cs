using Client.Web.IServices;
using Client.Web.Model;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Client.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
        }

        public async Task<string> GetTokenAsync()
        {
            // Lấy token từ local storage hoặc session
            return "jwt_token_here"; // Thay bằng logic thực tế
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", new { username, password });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                _authStateProvider.NotifyUserAuthentication(result.accessToken);
                return result;
            }
            throw new Exception("Login failed");
        }

        public async Task LogoutAsync()
        {
            _authStateProvider.NotifyUserLogout();
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Claim>> ParseClaimsFromJwt(string token)
        {
            return new List<Claim>();
        }
    }
}
