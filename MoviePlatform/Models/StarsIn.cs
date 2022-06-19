using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviePlatform.Models
{
    public class StarsIn
    {
        [Required]
        [Range(1, double.MaxValue)]
        public double Salary { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int? Duration { get; set; }
        public virtual int IdMovie { get; set; }      
        public virtual int IdPerson { get; set; }
        [ForeignKey("IdMovie")]
        public virtual Movie Movie { get; set; }
        [ForeignKey("IdPerson")]
        public virtual Person Actor { get; set; }

    }
}
