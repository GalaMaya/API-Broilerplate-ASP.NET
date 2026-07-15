namespace Boilerplate_ASPNet_API.Application.DTOs;

public record CreateUserRequest(
    string Name,
    string Email,
    string Password,
    int Status
);