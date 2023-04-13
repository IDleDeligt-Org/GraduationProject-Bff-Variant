using GraduationProject.Models.CocktailDB;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;
using GraduationProject.Models;

namespace GraduationProject.CocktailDB
{
    public class CocktailDBApi : ICocktailDBApi
    {
        private readonly HttpClient _httpClient;

        public CocktailDBApi(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<Beverage>> GetBeverages(string search)
        {
            HttpResponseMessage? response = await _httpClient.GetAsync($"api/json/v1/1/search.php?s={search}");

            if(response.IsSuccessStatusCode)
            {
                BeveragesApiResponse? result = await response.Content.ReadFromJsonAsync<BeveragesApiResponse>();

                List<Beverage> beverages = new List<Beverage>() { };

                if(result?.drinks != null) {
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

    }
}
