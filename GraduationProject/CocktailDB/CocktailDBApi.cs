using GraduationProject.Models.CocktailDB;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;
using GraduationProject.Models;
using Microsoft.IdentityModel.Tokens;
using GraduationProject.CocktailDB.FilterInformation;
using System.Linq;

namespace GraduationProject.CocktailDB
{
    public class CocktailDBApi : ICocktailDBApi
    {
        private readonly HttpClient _httpClient;
        private readonly IngredientsList _ingredientsList = new();

        public CocktailDBApi(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<Beverage>> GetBeveragesByName(string search)
        {
            HttpResponseMessage? response = await _httpClient.GetAsync($"api/json/v1/1/search.php?s={search}");

            if(response.IsSuccessStatusCode)
            {
                BeveragesApiResponse? result = await response.Content.ReadFromJsonAsync<BeveragesApiResponse>();
                List<Beverage> beverages = new();

                if (result?.drinks != null)
                {
                    foreach (BeverageApiResponse apiDrink in result?.drinks!)
                    {
                        beverages.Add(DrinkMapper.DrinkToBeverage(apiDrink));
                    }
                }
                return beverages;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve searchinformation. Status code: {response.StatusCode}");
            }
        }

        public async Task <Beverage> GetBeverageById(int id)
        {
            HttpResponseMessage? response = await _httpClient.GetAsync($"api/json/v1/1/lookup.php?i={id}");

            if (response.IsSuccessStatusCode)
            {
                BeveragesApiResponse? result = await response.Content.ReadFromJsonAsync<BeveragesApiResponse>();

                Beverage beverage = new();
                if (result?.drinks != null)
                {
                    beverage = DrinkMapper.DrinkToBeverage(result.drinks.First());
                }
                return beverage;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve searchinformation. Status code: {response.StatusCode}");
            }
        }

        public async Task<List<Beverage>> GetBeveragesByIngredient(string search)
        {
            try
            {
                HttpResponseMessage? response = await _httpClient.GetAsync($"api/json/v1/1/filter.php?i={search}");

                if (response.IsSuccessStatusCode)
                {
                    BeveragesApiResponse? result = await response.Content.ReadFromJsonAsync<BeveragesApiResponse>();
                    List<Beverage> beverages = new();
                    if (result?.drinks != null)
                    {
                        foreach (BeverageApiResponse beverageApiResponse in result.drinks)
                        {
                            Beverage beverage = await GetBeverageById(beverageApiResponse.idDrink);
                            beverages.Add(beverage);
                            // Add sleep
                        }
                    }
                    return beverages;
                }
                else
                {
                    return new List<Beverage>();
                }
            }
            catch(Exception e) {
                Console.WriteLine($"Failed to retrieve search information: {e}");
                return new List<Beverage>();
            };
        }

       
        public async Task<List<Beverage>> GetAllNonAlcoholicDrinks()
        {

            HttpResponseMessage? response = await _httpClient.GetAsync($"api/json/v1/1/filter.php?a=Non_Alcoholic");
            if (response.IsSuccessStatusCode)
            {
                BeveragesApiResponse? result = await response.Content.ReadFromJsonAsync<BeveragesApiResponse>();
                List<Beverage> beverages = new();
                if (result?.drinks != null)
                {
                    foreach(BeverageApiResponse beverageApiResponse in result.drinks)
                        {
                        Beverage beverage = await GetBeverageById(beverageApiResponse.idDrink);
                        beverages.Add(beverage);
                    }
                }
                return beverages;
            }

            else
            {
                throw new HttpRequestException($"Failed to retrieve searchinformation. Status code: {response.StatusCode}");
            }
        }
    }
}
