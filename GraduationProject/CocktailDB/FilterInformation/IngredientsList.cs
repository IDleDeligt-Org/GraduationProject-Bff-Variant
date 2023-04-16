using GraduationProject.Models;

namespace GraduationProject.CocktailDB.FilterInformation
{
    public class IngredientsList
    {
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>() {
        new Ingredient{Name = "Light rum"},
        new Ingredient{Name = "Applejack"},
        new Ingredient{Name = "Gin"},
        new Ingredient{Name = "Dark rum"},
        new Ingredient{Name = "Sweet Vermouth"},
        new Ingredient{Name = "Strawberry schnapps"},
        new Ingredient{Name = "Scotch"},
        new Ingredient{Name = "Apricot brandy"},
        new Ingredient{Name = "Triple sec"},
        new Ingredient{Name = "Southern Comfort"},
        new Ingredient{Name = "Orange bitters"},
        new Ingredient{Name = "Brandy"},
        new Ingredient{Name = "Lemon vodka"},
        new Ingredient{Name = "Blended whiskey"},
        new Ingredient{Name = "Dry Vermouth"},
        new Ingredient{Name = "Amaretto"},
        new Ingredient{Name = "Tea"},
        new Ingredient{Name = "Champagne"},
        new Ingredient{Name = "Coffee liqueur"},
        new Ingredient{Name = "Bourbon"},
        new Ingredient{Name = "Tequila"},
        new Ingredient{Name = "Vodka"},
        new Ingredient{Name = "Añejo rum"},
        new Ingredient{Name = "Bitters"},
        new Ingredient{Name = "Sugar"},
        new Ingredient{Name = "Kahlua"},
        new Ingredient{Name = "demerara Sugar"},
        new Ingredient{Name = "Dubonnet Rouge"},
        new Ingredient{Name = "Watermelon"},
        new Ingredient{Name = "Lime juice"},
        new Ingredient{Name = "Irish whiskey"},
        new Ingredient{Name = "Apple brandy"},
        new Ingredient{Name = "Carbonated water"},
        new Ingredient{Name = "Cherry brandy"},
        new Ingredient{Name = "Creme de Cacao"},
        new Ingredient{Name = "Grenadine"},
        new Ingredient{Name = "Port"},
        new Ingredient{Name = "Coffee brandy"},
        new Ingredient{Name = "Red wine"},
        new Ingredient{Name = "Rum"},
        new Ingredient{Name = "Grapefruit juice"},
        new Ingredient{Name = "Ricard"},
        new Ingredient{Name = "Sherry"},
        new Ingredient{Name = "Cognac"},
        new Ingredient{Name = "Sloe gin"},
        new Ingredient{Name = "Apple juice"},
        new Ingredient{Name = "Pineapple juice"},
        new Ingredient{Name = "Lemon juice"},
        new Ingredient{Name = "Sugar syrup"},
        new Ingredient{Name = "Milk"},
        new Ingredient{Name = "Strawberries"},
        new Ingredient{Name = "Chocolate syrup"},
        new Ingredient{Name = "Yoghurt"},
        new Ingredient{Name = "Mango"},
        new Ingredient{Name = "Ginger"},
        new Ingredient{Name = "Lime"},
        new Ingredient{Name = "Cantaloupe"},
        new Ingredient{Name = "Berries"},
        new Ingredient{Name = "Grapes"},
        new Ingredient{Name = "Kiwi"},
        new Ingredient{Name = "Tomato juice"},
        new Ingredient{Name = "Cocoa powder"},
        new Ingredient{Name = "Chocolate"},
        new Ingredient{Name = "Heavy cream"},
        new Ingredient{Name = "Galliano"},
        new Ingredient{Name = "Peach Vodka"},
        new Ingredient{Name = "Ouzo"},
        new Ingredient{Name = "Coffee"},
        new Ingredient{Name = "Spiced rum"},
        new Ingredient{Name = "Water"},
        new Ingredient{Name = "Espresso"},
        new Ingredient{Name = "Angelica root"},
        new Ingredient{Name = "Orange"},
        new Ingredient{Name = "Cranberries"},
        new Ingredient{Name = "Johnnie Walker"},
        new Ingredient{Name = "Apple cider"},
        new Ingredient{Name = "Everclear"},
        new Ingredient{Name = "Cranberry juice"},
        new Ingredient{Name = "Egg yolk"},
        new Ingredient{Name = "Egg"},
        new Ingredient{Name = "Grape juice"},
        new Ingredient{Name = "Peach nectar"},
        new Ingredient{Name = "Lemon"},
        new Ingredient{Name = "Firewater"},
        new Ingredient{Name = "Lemonade"},
        new Ingredient{Name = "Lager"},
        new Ingredient{Name = "Whiskey"},
        new Ingredient{Name = "Absolut Citron"},
        new Ingredient{Name = "Pisco"},
        new Ingredient{Name = "Irish cream"},
        new Ingredient{Name = "Ale"},
        new Ingredient{Name = "Chocolate liqueur"},
        new Ingredient{Name = "Midori melon liqueur"},
        new Ingredient{Name = "Sambuca"},
        new Ingredient{Name = "Cider"},
        new Ingredient{Name = "Sprite"},
        new Ingredient{Name = "7-Up"},
        new Ingredient{Name = "Blackberry brandy"},
        new Ingredient{Name = "Peppermint schnapps"},
        new Ingredient{Name = "Creme de Cassis"}
        };

        public bool CheckIngredient(string ingredient)
        {
            return Ingredients.Any(i => i.Name.Contains(ingredient, StringComparison.OrdinalIgnoreCase));
        }
        //private readonly ICocktailDBApi _cocktail;

        //public IngredientsList(ICocktailDBApi cocktailDBApi)
        //{
        //    _cocktail = cocktailDBApi; 
        //}

        //public async Task LoadAllIngredients()
        //{
        //    Ingredients = await _cocktail.GetAllIngredients();
        //}
    }
}
