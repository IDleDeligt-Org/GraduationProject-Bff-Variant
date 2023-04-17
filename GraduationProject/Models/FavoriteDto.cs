using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class FavoriteDto
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int FavoriteBeverageId { get; set; }

        [Required]
        public BeverageSource Source { get; set; }
    }
}
