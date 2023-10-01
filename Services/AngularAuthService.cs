using Microsoft.IdentityModel.Tokens;
using PersonalBook.API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PersonalBook.API.Services
{
    public class PasswordHash
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Iteration = 10000;

        public static string HashPassword(string Password)
        {
            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);
            var key = new Rfc2898DeriveBytes(Password, salt, Iteration);
            var hash = key.GetBytes(HashSize);
            var hasBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hasBytes, 0, SaltSize);
            Array.Copy(hash, 0, hasBytes, SaltSize, HashSize);
            var base64Hash = Convert.ToBase64String(hasBytes);

            return base64Hash;
        }

        public static bool VerifyPassword(string Password, string base64Hash) 
        {
            var hashByte = Convert.FromBase64String(base64Hash);
            var salt = new byte[SaltSize];
            Array.Copy(hashByte, 0, salt, 0, SaltSize);
            var key = new Rfc2898DeriveBytes(Password, salt, Iteration);
            byte[] hash = key.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashByte[i+SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class AngularAuthService
    {
        public string CreateJWT(ApplicationUser appllicationUser)
        {
            JwtSecurityTokenHandler jwtHangler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("thisisakeywhocanread");
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, appllicationUser.Id.ToString()),
                new Claim(ClaimTypes.Role, appllicationUser.Role),
                new Claim(ClaimTypes.Actor, $"{appllicationUser.FirstName} {appllicationUser.LastName}")
            });
            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredentials,
            };
            SecurityToken token = jwtHangler.CreateToken(securityTokenDescriptor);

            return jwtHangler.WriteToken(token);
        }
    }
}
