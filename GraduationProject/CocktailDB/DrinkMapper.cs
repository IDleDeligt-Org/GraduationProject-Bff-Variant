using GraduationProject.Models;
using GraduationProject.Models.CocktailDB;


public static class DrinkMapper
{
    public static Beverage DrinkToBeverage(DrinkApiResponse apiDrink)
    {
        Beverage beverage = new()
        {
            BeverageId = apiDrink.idDrink,
            Name = apiDrink.strDrink,
            Tag = apiDrink.strCategory,
            Alcohol = (apiDrink.strAlcoholic.ToLower().Contains("alcohol") ? true : false),
            Glass = apiDrink.strGlass,
            Video = apiDrink.strVideo,
            Instruction = apiDrink.strInstructions,
            Image = apiDrink.strDrinkThumb,
            ImageAttribution = apiDrink.strImageAttribution,
            CreativeCommonsConfirmed = (apiDrink.strCreativeCommonsConfirmed.ToLower() == "yes" ? true : false),
            BeverageIngredients = new List<BeverageIngredient>()
        };

        for (int i = 1; i <= 15; i++)
        {
            string? ingredientName = (string)apiDrink.GetType().GetProperty($"strIngredient{i}")!.GetValue(apiDrink, null)!;
            if (string.IsNullOrEmpty(ingredientName))
            {
                break;
            }

            string? ingredientMeasure = (string)apiDrink.GetType().GetProperty($"strMeasure{i}")!.GetValue(apiDrink, null)!;

            beverage.BeverageIngredients.Add(new BeverageIngredient
            {
                Ingredient = new Ingredient
                {
                    Name = ingredientName,
                    
                },
                Measurment = ingredientMeasure,
            });
        }

        return beverage;
    }
}
