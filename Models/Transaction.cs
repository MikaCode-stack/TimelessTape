using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }  // Primary Key
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string TitleId { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(10)]
        public string Status { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime RentalDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReturnDate { get; set; }

        // Foreign Keys
        public User Customer { get; set; }
        public Title Title { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Latefee> Latefees { get; set; }
        
    }
}
