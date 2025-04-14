using System.ComponentModel.DataAnnotations;

namespace TimelessTapes.Models
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
       
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be between 8 and 100 characters.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }
    }
}
