using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviePlatform.Models
{
    public class Review : Comment, IRating
    {
        [Key]
        public int IdReview { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public Grade Grade { get; set; }
        public virtual int IdUser { get; set; }       
        public virtual int IdMovie { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
        [ForeignKey("IdMovie")]
        public virtual Movie Movie { get; set; }
        
    }
}
