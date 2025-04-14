using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models
{
    public class Latefee
    {
        [Key]
        public int LatefeeId { get; set; }
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public double Amount { get; set; }
        public bool Paid { get; set; }      
        public DateTime FeeDate { get; set; }
        //Foreign Keys navigation properties
        public Transaction Transaction { get; set; }
    }
}
