using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using TimelessTapes.Data;

namespace TimelessTapes.Models

{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public required string AccessType { get; set; }
        public object? PasswordHash { get; internal set; }

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
                Password = password,
                AccessType = accessType,
                CreatedAt = DateTime.UtcNow
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return newUser.UserId;
        }

        public string? GetAccessType()
        {
            return AccessType;
        }

        public string Login(string email, string password)
        {
            if (Email == email && Password == password)
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
