using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Beverage
    {
        public int BeverageId { get; set; } 
        public int ApiId { get; set; }

        [Required(ErrorMessage = "Please enter a name for the beverage")]
        [StringLength (50, ErrorMessage = "Beverage name cannot be longer than 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description for the beverage")]
        [StringLength (100, ErrorMessage = "Beverage description cannot be longer than 500 characters")]
        public string? Tag { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please specify if the beverage has alcohol or is alcohol free")]
        public bool Alcohol { get; set; }

        public GlassType Glass { get; set; }

        [Required(ErrorMessage = "Please enter the instructions to make the beverage")]
        [StringLength (1500, ErrorMessage = "Instructions cannot be longer than 1500 characters")]
        public string Instruction { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please copy the url link to your beverage image")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; } = string.Empty;

        [DataType(DataType.Url)]
        public string? Video { get; set; } 

        public string? ImageAttribution { get; set; }
        public bool CreativeCommonsConfirmed { get; set; }

        //Navigation Properties
        public virtual ICollection<BeverageIngredient> BeverageIngredients { get; set; }

        //[Required]
        public BeverageSource Source { get; set; }
    }
}
