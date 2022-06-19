using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviePlatform.Models
{
    public class Movie
    {
        public enum Genre
        {
            Action,
            Comedy,
            Drama,
            Fantasy,
            Horror,
            Thriller
        }
        [Key]
        public int IdMovie { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MinLength(1)]
        public string[] Genres { get; set; }
        public virtual ICollection<StarsIn> Roles { get; set;}
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual int IdWriter { get; set; }
        public virtual int IdDirector { get; set; }
        [ForeignKey("IdWriter")]
        public virtual Person Writer { get; set; }
        [ForeignKey("IdDirector")]
        public virtual Person Director { get; set; }
    }
}
