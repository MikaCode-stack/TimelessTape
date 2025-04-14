using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using TimelessTapes.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
//Users model. Ensures that the user has a unique ID, name, email, password hash and salt, access type, and created date.
namespace TimelessTapes.Models
{
    public class User
    {
        public int UserId { get; set; }

        
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string AccessType { get; set; } = "Customer";
        public DateTime CreatedAt { get; set; }

        // Navigation properties for Foreign Keys
        public ICollection<AdminLog> AdminLog { get; set; } 
        public ICollection<CustomerReview> CustomerReviews { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Refund> Refunds { get; set; }
        public ICollection<Transaction> Transaction { get; set; }

    }

}
    



