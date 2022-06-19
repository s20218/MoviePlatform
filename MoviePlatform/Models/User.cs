using System.ComponentModel.DataAnnotations;

namespace MoviePlatform.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        [MaxLength(10)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        public string? ProfileDescription { get; set; }
        [Required]
        public UserType UserType { get; set; }
        public virtual ICollection<Report> ReportsSent { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ClosedReport> ReportsClosed { get; set; }
    }
}
