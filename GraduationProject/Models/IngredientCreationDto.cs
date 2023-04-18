using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class IngredientCreationDto
    {
        public int IngredientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
