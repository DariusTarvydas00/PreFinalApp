using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Services
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public string GenerateJwtToken(string user,int userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetJwtSecretKey()));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                GetJwtIssuer(),
                GetJwtAudience(),
                claims,
                expires: DateTime.Now.AddSeconds(GetJwtExpirationSeconds()),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return StructuralComparisons.StructuralEqualityComparer.Equals(computedHash, storedHash);
        }

        public User CreateUser(string username, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            return new User
            {
                UserName = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private string GetJwtSecretKey() => configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt secret key is missing in configuration.");

        private string GetJwtIssuer() => configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt issuer is missing in configuration.");

        private string GetJwtAudience() => configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt audience is missing in configuration.");

        private int GetJwtExpirationSeconds()
        {
            var expirationSeconds = configuration["Jwt:ExpirationSeconds"];
            return expirationSeconds != null ? int.Parse(expirationSeconds) : throw new InvalidOperationException("Jwt expiration seconds are missing in configuration.");
        }
    }
}
