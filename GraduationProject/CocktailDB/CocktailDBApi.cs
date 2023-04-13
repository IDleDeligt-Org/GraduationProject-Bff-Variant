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
                var result = await response.Content.ReadFromJsonAsync<BeverageApiResponse>();

                List<Beverage> drinks = new List<Beverage>() { };

                if(result?.drinks != null) {
                    foreach (BeverageApiResponse apiDrink in result?.drinks!)
                    {
                        drinks.Add(DrinkMapper.ApiResponseToBeverage(apiDrink));
                    }
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
