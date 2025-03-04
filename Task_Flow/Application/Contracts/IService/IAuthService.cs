using Shared.Models;

namespace Application.Contracts.IService
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterRequest registerRequest);
        Task<string> LoginAsync(string email, string password);
    }
}
