using System.ComponentModel.DataAnnotations;

namespace MoviePlatform.Models
{
    public class Person
    {
        [Key]
        public int IdPerson { get; set; }
        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(10)]
        public string LastName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [MinLength(1)]
        public string[] Types { get; set; }
        public bool? HasDegree { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<Movie> MoviesDirected { get; set; }
        public virtual ICollection<Movie> MoviesWritten{ get; set; }
        public virtual ICollection<StarsIn> Roles { get; set; }
        
    }
}
