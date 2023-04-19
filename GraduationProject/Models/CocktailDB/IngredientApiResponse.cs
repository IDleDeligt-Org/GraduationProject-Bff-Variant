namespace GraduationProject.Models.CocktailDB
{
    public class IngredientsApiResponse
    {
        public IEnumerable<IngredientApiResponse>? drinks { get; set; }
    }

    public class IngredientApiResponse
    {
        public string? strIngredient1 { get; set; }
        public string? strDescription { get; set; } // You can add the corresponding property from the API

    }
}
