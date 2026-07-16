using Boilerplate_ASPNet_API.Application.DTOs;
using Boilerplate_ASPNet_API.Domain.Entities;

namespace Boilerplate_ASPNet_API.Application.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(CreateUserRequest request);
    
    Task<User?> GetUserByEmailAsync(string email);

    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);

    Task<UserResponseDto> UpdateUserAsync(Guid Id, UpdateUserRequest request);

    Task DeleteUserAsync(Guid Id);
}