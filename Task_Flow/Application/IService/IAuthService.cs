using Shared.Models;

namespace Application.IService
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterRequest registerRequest);
        Task<string> LoginAsync(string email, string password);
    }
}
