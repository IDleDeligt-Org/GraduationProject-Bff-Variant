using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

       
        //Navigation Properties
        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
