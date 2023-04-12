using GraduationProject.Models.CocktailDB;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;
using System.Text.Json;

namespace GraduationProject.CocktailDB
{
    public class CocktailDBApi : ICocktailDBApi
    {
        private readonly HttpClient _httpClient;

        public CocktailDBApi(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<Drink>> GetDrinks(string search)
        {
            var response = await _httpClient.GetAsync($"api/json/v1/1/search.php?s={search}");

            if(response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<List<DrinkApiResponse>>(stringResponse,
                        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                List<Drink> drinks = new();
                foreach (DrinkApiResponse apiDrink in result)
                {
                    drinks.Add(MapToDrinkModel(apiDrink));
                }
                return drinks;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve searchinformation. Status code: {response.StatusCode}");
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
