using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models

{

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public string AccessType { get; set; } = "User";
        public DateTime CreatedAt { get; set; }

        public ICollection<AdminLog> AdminLog { get; set; } // One user can have multiple admin logs
        public ICollection<CustomerReview> CustomerReviews { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Refund> Refunds { get; set; }
        public ICollection<Transaction> Transaction { get; set; }

    }

}
    


