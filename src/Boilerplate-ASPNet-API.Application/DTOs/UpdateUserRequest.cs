namespace Boilerplate_ASPNet_API.Application.DTOs;

public record UpdateUserRequest
(
    string Name,
    string Email,
    string Status
);