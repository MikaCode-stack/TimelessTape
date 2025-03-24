using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models
{
    public class Video
    {

        [Key]
        public int VideoId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseYear { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int AvailableCopies { get; set; }

        public ICollection<CustomerReview> CustomerReviews { get; set; }
        public ICollection<Transaction> Transaction { get; set; }


    }
}
