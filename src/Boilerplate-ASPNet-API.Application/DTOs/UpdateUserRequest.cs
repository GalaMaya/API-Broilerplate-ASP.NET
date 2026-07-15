namespace Boilerplate_ASPNet_API.Application.DTOs;

public record UpdateUserRequest
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public int Status { get; init; }
}