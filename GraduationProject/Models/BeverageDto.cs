using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class BeverageDto
    {
        public int? BeverageId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Tag { get; set; }
        [Required]
        public bool Alcohol { get; set; }
        public GlassType Glass { get; set; }

        [Required]
        public string Instruction { get; set; }

        [Required]
        public string Image { get; set; }
        public string? Video { get; set; }
        public string? ImageAttribution { get; set; }
        public bool CreativeCommonsConfirmed { get; set; }
        public ICollection<BeverageIngredientDto> BeverageIngredients { get; set; }

    }
}

