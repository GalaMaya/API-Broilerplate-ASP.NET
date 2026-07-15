namespace Boilerplate_ASPNet_API.Application.DTOs;

public record UserResponseDto(
    Guid Id,
    string Name,
    string Email,
    int Status,
    DateTime CreatedAt
);