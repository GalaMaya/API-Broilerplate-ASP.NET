namespace Boilerplate_ASPNet_API.Application.DTOs;

public record LoginResponse(
    Guid Id,
    string Name,
    string Email,
    string Token
);