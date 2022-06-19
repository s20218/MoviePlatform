using System.ComponentModel.DataAnnotations;

namespace MoviePlatform.Models
{
    public abstract class Comment
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Maximum {1} characters allowed.")]
        public string Text { get; set; }
    }
}
