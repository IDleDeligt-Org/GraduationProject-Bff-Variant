namespace GraduationProject.Models.CocktailDB
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Tag { get; set; }
        public bool Alcohol { get; set; }
        public string? Glass { get; set; }
        public string? Video { get; set; }
        public string Instruction { get; set; }
        public string Image { get; set; }

        public List<Ingredient> Ingredients { get; set; }
       
       public string? ImageAttribution { get; set; }
        public bool CreativeCommonsConfirmed { get; set; }
    }
}
