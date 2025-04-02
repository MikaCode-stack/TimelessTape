using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }  

        [Required]
        public int CustomerID { get; set; } 

        [Required]
        public int VideoID { get; set; }  

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; } = 0.00m;  

        [Required]
        [StringLength(10)]
        public string Status { get; set; } = "Active";  

        [Required]
        [Column(TypeName = "date")]
        public DateTime RentalDate { get; set; }  

        [Column(TypeName = "date")]
        public DateTime? ReturnDate { get; set; }  
    }
}
