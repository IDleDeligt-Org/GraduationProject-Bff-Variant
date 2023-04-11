using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class User
    {
        
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter your user name")]
        [StringLength(50, ErrorMessage = "User name must be cannot be longer than 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(50, ErrorMessage = "Password cannot be longer than 50 characters")]
        [RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[@$!%?&])[A-Za-z\d@$!%?&]{8,}$"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [StringLength(50, ErrorMessage = "Email cannot be longer than 100 characters")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //Navigation Properties
        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
