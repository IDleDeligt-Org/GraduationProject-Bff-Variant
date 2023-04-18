using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class BeverageIngredientDto
    {
        public int BeverageIngredientId { get; set; }
        public int BeverageId { get; set; }
        public int IngredientId { get; set; }

        [Required]
        public string Measurement { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [Required]
        public IngredientDto Ingredient { get; set; }
    }
}

