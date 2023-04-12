using GraduationProject.Models.CocktailDB;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;

namespace GraduationProject.CocktailDB
{
    public class CocktailDBApi : ICocktailDBApi
    {
        private readonly HttpClient _httpClient;

        public CocktailDBApi()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("www.thecocktaildb.com/")
            };
        }

        public async Task<List<Drink>> GetDrinks(string search)
        {
            string url = string.Format("api/json/v1/1/search.php?s={0}", search);
            List<DrinkApiResponse> result = new();
            var response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                await response.Content.ReadFromJsonAsync<DrinkApiResponse>();

                List<Drink> drinks = new();
                foreach(DrinkApiResponse apiDrink in result)
                {
                    drinks.Add(MapToDrinkModel(apiDrink));
                }
                return drinks;
            }
            else
            {
                throw new Exception($"Failed to retrieve searchinformation. Status code: {response.StatusCode}");
            }
        }

        public Drink MapToDrinkModel(DrinkApiResponse apiDrink)
        {
            Drink drink = new()
            {
                Id = apiDrink.idDrink,
                Name = apiDrink.strDrink,
                Tag = apiDrink.strCategory,
                Alcohol = (apiDrink.strAlcoholic.ToLower().Contains("alcohol") ? true : false),
                Glass = apiDrink.strGlass,
                Video = apiDrink.strVideo,
                Instruction = apiDrink.strInstructions,
                Image = apiDrink.strDrinkThumb,
                ImageAttribution = apiDrink.strImageAttribution,
                CreativeCommonsConfirmed = (apiDrink.strCreativeCommonsConfirmed.ToLower() == "yes" ? true : false),
                Ingredients = new List<Ingredient>()
            };

            for (int i = 1; i <= 15; i++)
            {
                string? ingredientName = (string)apiDrink.GetType().GetProperty($"strIngredient{i}")!.GetValue(apiDrink,null)!;
                if (string.IsNullOrEmpty(ingredientName))
                {
                    break;
                }

                string? ingredientMeasure = (string)apiDrink.GetType().GetProperty($"strMeasure{i}")!.GetValue(apiDrink, null)!;

                drink.Ingredients.Add(new Ingredient
                {
                    Name = ingredientName,
                    Measurment = ingredientMeasure,
                });
            }

            return drink;
        }
    }
}
