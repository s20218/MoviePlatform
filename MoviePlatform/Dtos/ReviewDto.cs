using MoviePlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviePlatform.Dtos
{
    public class ReviewDto
    {
        [Required]
        public Movie Movie { get; set; }
        public string Grade { get; set; }
        public string Opinion { get; set; }
    }
}
