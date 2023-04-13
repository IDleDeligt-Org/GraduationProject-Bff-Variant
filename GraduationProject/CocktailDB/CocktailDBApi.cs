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
            var response = await _httpClient.GetAsync($"api/json/v1/1/search.php?s={search}");

            if(response.IsSuccessStatusCode)
            {
                var newResult = await response.Content.ReadFromJsonAsync<DrinksApiResponse>();

                List<Beverage> drinks = new List<Beverage>();

                foreach(DrinkApiResponse apiDrink in newResult?.drinks!)
                {
                    drinks.Add(DrinkMapper.DrinkToBeverage(apiDrink));
                }
                return drinks;
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve searchinformation. Status code: {response.StatusCode}");
            }
        }

       
    }
}
