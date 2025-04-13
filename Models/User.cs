using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using TimelessTapes.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace TimelessTapes.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [NotMapped] 
        public string? Password { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public byte[] PasswordSalt { get; set; } = new byte[0];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public required string AccessType { get; set; }

        // register a new user
        public static async Task<int> Register(DBHandler context, string name, string email, string password, string accessType)
        {
            if (context.Users.Any(u => u.Email == email))
            {
                throw new Exception("Email is already registered.");
            }

           
            var newUser = new User
            {
                Name = name,
                Email = email,
                AccessType = accessType,
                CreatedAt = DateTime.UtcNow
            };

            
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            newUser.PasswordSalt = salt;

            
            newUser.PasswordHash = HashPassword(password, salt);

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return newUser.UserId;
        }

        
        private static string HashPassword(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        // password verification
        public bool VerifyPassword(string password)
        {
            string computedHash = HashPassword(password, PasswordSalt);
            return computedHash == PasswordHash;
        }

        public string? GetAccessType()
        {
            return AccessType;
        }

        // login method
        public string Login(string email, string password)
        {
            if (Email == email && VerifyPassword(password))
            {
                return AccessType;
            }
            return "Guest";
        }

        //logout
        public void Logout()
        {
            // clear session or authentication token
        }
    }
}