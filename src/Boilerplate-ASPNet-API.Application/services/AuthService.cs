using Boilerplate_ASPNet_API.Application.DTOs;
using Boilerplate_ASPNet_API.Application.Interfaces;
using Boilerplate_ASPNet_API.Domain.Entities;
using Boilerplate_ASPNet_API.Application.Common.Extensions;

namespace Boilerplate_ASPNet_API.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    // login
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        if (user.Status != 0)
        {
            throw new InvalidOperationException("Your Account inactive.");
        }

        return new LoginResponse(
            user.Id,
            user.Name,
            user.Email,
            "mock_token_jwt_123"
        );
    }
}