using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models
{
    public class CustomerReview
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public int VideoId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        //Foreign Keys navigation properties
        public Video Video { get; set; }
        public User User { get; set; }
    }
}
