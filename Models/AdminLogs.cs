using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimelessTapes.Models
{
    public class AdminLog
    {
        [Key]
        public int LogId { get; set; }
        [Required]
        public string AdminId { get; set; }
        [Required]
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        //Foreign Key navigation properties
        public User User { get; set; }
    }
}
