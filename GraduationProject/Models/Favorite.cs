using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Favorite
    {

        public int FavoriteId { get; set; }
        public int BeverageId { get; set; }
        public int UserId { get; set; }
        
       


        //Navigation Properties
        public virtual Beverage Beverage { get; set; }
        public virtual User User { get; set; }
        
        [Required]
        public BeverageSource Source { get; set; }
    }
}
