using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class IngredientDto
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
