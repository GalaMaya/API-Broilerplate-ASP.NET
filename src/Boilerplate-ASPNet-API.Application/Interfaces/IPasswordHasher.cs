namespace Boilerplate_ASPNet_API.Application.Interfaces;

public interface IPasswordHasher
{
    // Mengubah password plain text menjadi hash
    string HashPassword(string plainTextPassword);

    // Membandingkan password plain text dengan hash yang ada di database
    bool VerifyPassword(string plainTextPassword, string hashedPassword);
}