using GraduationProject.Models;
using GraduationProject.Models.CocktailDB;

public static class DrinkMapper 
{

    public static Beverage DrinkToBeverage(BeverageApiResponse apiDrink)
    {
        Beverage beverage = new(){
            ApiId = apiDrink.idDrink,
            Name = apiDrink.strDrink!,
            Tag = apiDrink.strCategory + (apiDrink.strTags != null ? ", " + apiDrink.strTags : ""),
            Alcohol = (!string.IsNullOrEmpty(apiDrink.strAlcoholic)
                       && !apiDrink.strAlcoholic.ToLower().Contains("non")),
            Glass = ParseGlassType(apiDrink.strGlass),
            Video = apiDrink.strVideo,
            Instruction = apiDrink.strInstructions!,
            Image = apiDrink.strDrinkThumb!,
            ImageAttribution = apiDrink.strImageAttribution,
            CreativeCommonsConfirmed = (!string.IsNullOrEmpty(apiDrink.strCreativeCommonsConfirmed) 
                                        && apiDrink.strCreativeCommonsConfirmed!.ToLower() == "yes"),
            BeverageIngredients = new List<BeverageIngredient>(),
            Source = BeverageSource.CocktailDB,
        };

        //for (int i = 1; i <= 15; i++)
        //{
        //    string? ingredientName = (string)apiDrink
        //        .GetType()
        //        .GetProperty($"strIngredient{i}")!
        //        .GetValue(apiDrink, null)!;
        //    if (string.IsNullOrEmpty(ingredientName)){
        //        break;
        //    }

        //    string? ingredientMeasure = (string)apiDrink
        //        .GetType()
        //        .GetProperty($"strMeasure{i}")!
        //        .GetValue(apiDrink, null)!;

        //    beverage.BeverageIngredients.Add(new BeverageIngredient{
        //        Ingredient = new Ingredient{
        //            Name = ingredientName,
        //        },
        //        Measurement = ingredientMeasure,
        //    });
        //}

        for (int i = 1; i <= 15; i++) {
            string? ingredientName = (string)apiDrink
                .GetType()
                .GetProperty($"strIngredient{i}")!
                .GetValue(apiDrink, null)!;
            if (string.IsNullOrEmpty(ingredientName)) {
                break;
            }

            var measureProp = apiDrink.GetType().GetProperty($"strMeasure{i}");
            var rawMeasure = measureProp?.GetValue(apiDrink);
            string ingredientMeasure = rawMeasure as string ?? "";

            beverage.BeverageIngredients.Add(new BeverageIngredient
            {
                Ingredient = new Ingredient
                {
                    Name = ingredientName,
                },
                Measurement = ingredientMeasure,
            });
        }

        return beverage;
    }
    public static Ingredient DrinkToIngredient(BeverageApiResponse apiDrink)
    {
        Ingredient ingredient = new()
        {
            Name = apiDrink.strIngredient1!,
        };
        return ingredient;
    }
    public static GlassType ParseGlassType(string? glass)
    {
        if (string.IsNullOrEmpty(glass))
        {
            return GlassType.Unknown;
        }

        return Enum.TryParse(glass.Replace(" ", "_"), true, out GlassType glassType)
            ? glassType
            : GlassType.Unknown;
    }
}
