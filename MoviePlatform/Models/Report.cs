using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviePlatform.Models
{
    public class Report
    {
        [Key]
        public int IdReport { get; set; }
        [Required]
        public int IdReportedUser { get; set; }
        [Required]
        [MaxLength(50)]
        public string ReportDescription { get; set; }
        
        public virtual int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User? User{ get; set; }
    }
}
