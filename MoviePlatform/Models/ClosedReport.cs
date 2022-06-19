using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviePlatform.Models
{
    public class ClosedReport : Report
    {
        [Required]
        public Action Action { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Duration { get; set; }
        public virtual int IdModerator { get; set; }
        [ForeignKey("IdModerator")]
        public virtual User? Moderator { get; set; }
    }
}
