using Boilerplate_ASPNet_API.Application.Interfaces;
using BCrypt.Net;

namespace Boilerplate_ASPNet_API.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string plainTextPassword)
    {
        // BCrypt secara otomatis men-generate salt dan menggabungkannya dengan hash
        return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
    }

    public bool VerifyPassword(string plainTextPassword, string hashedPassword)
    {
        // BCrypt secara otomatis mengekstrak salt dari hashedPassword untuk verifikasi
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
    }
}