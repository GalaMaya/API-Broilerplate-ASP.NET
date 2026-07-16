using Boilerplate_ASPNet_API.Domain.Entities;

namespace Boilerplate_ASPNet_API.Application.Interfaces;

public interface ITokenService
{
    // Menghasilkan string JWT berdasarkan data User
    string GenerateToken(User user);
}