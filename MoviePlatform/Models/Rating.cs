using System.ComponentModel.DataAnnotations;

namespace MoviePlatform.Models
{
    public class Rating : IRating
    {
        [Required]
        public Grade Grade { get; set; }
    }
}
