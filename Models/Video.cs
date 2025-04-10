using System;
using System.ComponentModel.DataAnnotations;

namespace TimelessTapes.Models
{
    public class Video
    {
        public int VideoID { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Genre { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool Available { get; set; } = true;
    }
}

