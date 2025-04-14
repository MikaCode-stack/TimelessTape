using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;


namespace TimelessTapes.Models
{
    public class Title
    {
        [Key]

        [Name("show_id")]
        public string TitleId { get; set; }

        [Name("type")]
        public string TitleType { get; set; }

        [Name("title")]
        public string PrimaryTitle { get; set; }

        [Name("director")]
        public string Director { get; set; }

        [Name("cast")]
        public string Cast { get; set; }

        [Name("release_year")]
        public int? ReleaseYear { get; set; }

        [Name("rating")]
        public string Rating { get; set; }  // Nullable for missing values

        [Name("duration")]
        public string Duration { get; set; }

        [Name("listed_in")]
        public string Genres { get; set; }

        [Name("description")]
        public string Description { get; set; }

        [Optional]
        [Name("price")]
        public decimal Price { get; set; } = 2.99m;

        [Optional]
        [Name("copies")]
        public int Copies { get; set; } = 1;

        public ICollection<Transaction> Transaction { get; set; }
        public ICollection<CustomerReview> CustomerReviews { get; set; }
    }
}
