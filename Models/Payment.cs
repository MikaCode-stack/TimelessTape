using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace TimelessTapes.Models
{
    public class Payment
    {

        [Key]
        public int PaymentId { get; set; }
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }

        public User User { get; set; }
        public Transaction Transaction { get; set; }

        public ICollection<Refund> Refund { get; set; }
        
    }
}
