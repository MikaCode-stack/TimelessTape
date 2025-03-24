using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models
{
    public class Refund
    {
        [Key]
        public int RefundId { get; set; }
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public decimal RefundAmount { get; set; }
        [Required]
        public DateTime RefundDate { get; set; }
        public string Status { get; set; }
        [Required]
        //Foreign Keys navigation properties
        public Payment Payment { get; set; }
        public User User { get; set; }
    }
}
