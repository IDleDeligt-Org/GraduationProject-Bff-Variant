using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Please enter an ingredient name")]
        [StringLength(50, ErrorMessage = "Ingredient name cannot be longer than 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the description of the ingredient")]
        [StringLength(1500, ErrorMessage = "Ingredient description cannot be longer than 1500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please copy the url link to your ingredient image")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; } = string.Empty;


        //Navigation Properties
        public virtual ICollection<BeverageIngredient> BeverageIngredients { get; set; }
    }
}
