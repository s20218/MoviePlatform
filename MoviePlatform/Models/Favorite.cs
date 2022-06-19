using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviePlatform.Models
{
    public class Favorite
    {
        public virtual int IdMovie { get; set; }
        [ForeignKey("IdMovie")]
        public virtual Movie Movie { get; set; }
        
        public virtual int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User{ get; set; }

    }
}
