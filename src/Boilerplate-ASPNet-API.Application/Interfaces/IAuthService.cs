using Boilerplate_ASPNet_API.Application.DTOs;
using Boilerplate_ASPNet_API.Domain.Entities;

namespace Boilerplate_ASPNet_API.Application.Services;

public interface IUserService
{
	Task<LoginResponse> LoginAsync(LoginRequest request);
}