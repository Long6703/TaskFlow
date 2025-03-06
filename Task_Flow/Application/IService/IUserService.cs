using Application.DTOs;

namespace Application.IService
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserCreateDTO userCreateDTO);
        Task<string> LoginAsync(string email, string password);
        Task<IEnumerable<string>> GetUserPermissions(Guid userId);
    }
}
