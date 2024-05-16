using DataAccess.Models;

namespace Jwt.Services;

public interface IJwtService
{
    string GenerateJwtToken(string user,int userId);
    bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
    User CreateUser(string username, string password);
}