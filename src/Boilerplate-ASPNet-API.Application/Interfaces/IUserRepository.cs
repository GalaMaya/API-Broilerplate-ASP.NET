using Boilerplate_ASPNet_API.Domain.Entities;
namespace Boilerplate_ASPNet_API.Application.Interfaces;
public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task UpdateAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
}
