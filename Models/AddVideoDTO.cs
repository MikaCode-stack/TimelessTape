namespace TimelessTapes.Models
{
    public class AddVideoDTO
    {
        public string TitleId { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public int? ReleaseYear { get; set; }
        public string Rating { get; set; }
        public string Duration { get; set; }
        public string Genres { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Copies { get; set; }
    }

}
