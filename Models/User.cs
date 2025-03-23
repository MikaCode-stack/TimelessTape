using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models

{

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public int PasswordHash { get; set; }
        [Required]
        public string AccessType { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
