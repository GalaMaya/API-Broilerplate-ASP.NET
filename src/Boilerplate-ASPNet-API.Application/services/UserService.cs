using Boilerplate_ASPNet_API.Application.DTOs;
using Boilerplate_ASPNet_API.Application.Interfaces;
using Boilerplate_ASPNet_API.Domain.Entities;
using Boilerplate_ASPNet_API.Application.Common.Extensions;

namespace Boilerplate_ASPNet_API.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // ✅ Method ini PERSIS sama dengan kontrak di IUserService
    public async Task<User> CreateUserAsync(CreateUserRequest request)
    {
        // 1. Cek email
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email already registered.");
        }

        // 2. Buat Entity
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PasswordHash = $"HASHED_{request.Password}", // Nanti diganti BCrypt
            Status = request.Status,
            CreatedAt = DateTime.UtcNow
        };

        // 3. Simpan
        return await _userRepository.CreateAsync(newUser);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

   
    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }
        return user;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();

        return users.Select(u => new UserResponseDto(
            Id: u.Id,
            Name: u.Name,
            Email: u.Email,
            Status: u.Status,
            CreatedAt: u.CreatedAt
        ));
    }

    public async Task<UserResponseDto> UpdateUserAsync(Guid Id, UpdateUserRequest request)
    {

        var checkUser = await _userRepository.GetByIdAsync(Id);
        if (checkUser == null)
        {
            throw new KeyNotFoundException($"User with ID{Id} not found");
        }

        var existingUserWithEmail = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUserWithEmail != null && existingUserWithEmail.Id != Id)
        {
            throw new InvalidOperationException("Email Already Exist.");
        }

        

        checkUser.Name = request.Name;
        checkUser.Email = request.Email;
        checkUser.Status = request.Status;
        checkUser.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(checkUser);

        return new UserResponseDto(
            Id: checkUser.Id,
            Name: checkUser.Name,
            Email: checkUser.Email,
            Status: checkUser.Status,
            CreatedAt: checkUser.CreatedAt
        );
    }
}