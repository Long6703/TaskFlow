using Client.Web.IServices;
using Client.Web.Model;
using Client.Web.Model.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Security.Claims;

namespace Client.Web.Services
{
    public class AuthService : BaseApiService, IAuthService
    {
        private readonly CustomAuthStateProvider _authStateProvider;
        private readonly TokenManagementService _tokenManagementService;
        public AuthService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authStateProvider, TokenManagementService tokenManagementService) : base(httpClientFactory)
        {
            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
            _tokenManagementService = tokenManagementService;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            return await _tokenManagementService.GetAccessTokenAsync();
        }

        public async Task<string> GetRefreshTokenAsync()
        {
            return await _tokenManagementService.GetRefreshTokenAsync();
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var response = await PostAsync<LoginResponse>("/api/auth/login", new { username, password });
            await _tokenManagementService.SetTokensAsync(response.accessToken, response.refreshToken);
            _authStateProvider.NotifyUserAuthentication(response.accessToken);
            return response;
        }

        public async Task LogoutAsync()
        {
            await _tokenManagementService.RemoveTokensAsync();
            _authStateProvider.NotifyUserLogout();
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Claim>> ParseClaimsFromJwt(string token)
        {

            // This is just a sample code to show how to parse claims from JWT token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "user1"),
                new Claim("permissions", "ViewAll"),
                new Claim("permissions", "CreateTask")
            };
            return await Task.FromResult(claims);
        }

        public Task<bool> Register(UserCreateDTO userCreateDTO)
        {
            var response = PostAsync<bool>("api/user/register", userCreateDTO);
            return response;
        }
    }
}
