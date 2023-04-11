using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class BeverageIngredient
    {

        public int BeverageIngredientId { get; set; }
        public int BeverageId { get; set; }
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "Please enter the mesurments for the ingredient")]
        public string Measurment { get; set; }


        //Navigation Properties
        public virtual Beverage Beverage { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
