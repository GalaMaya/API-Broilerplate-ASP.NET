namespace Boilerplate_ASPNet_API.Application.DTOs;

public record LoginRequest(
    string Email,
    string Password
);