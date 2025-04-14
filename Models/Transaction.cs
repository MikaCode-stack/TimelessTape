using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }  // Primary Key
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string TitleId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Foreign Keys
        public User Customer { get; set; }
        public Title Title { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Latefee> Latefees { get; set; }
        
    }
}
