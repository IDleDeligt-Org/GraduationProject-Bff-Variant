using GraduationProject.Models.CocktailDB;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;
using GraduationProject.Models;

namespace GraduationProject.CocktailDB
{
    public class CocktailDBApi : ICocktailDBApi
    {
        private readonly HttpClient _httpClient;

        public CocktailDBApi()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/")
            };
        }

        public async Task<List<Beverage>> GetBeverages(string search)
        {
            string url = string.Format("api/json/v1/1/search.php?s={0}", search);
            var response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                var newResult = await response.Content.ReadFromJsonAsync<DrinksApiResponse>();

                List<Beverage> drinks = new List<Beverage>();

                foreach(DrinkApiResponse apiDrink in newResult?.drinks)
                {
                    drinks.Add(DrinkMapper.DrinkToBeverage(apiDrink));
                }
                return drinks;
            }
            else
            {
                throw new Exception($"Failed to retrieve searchinformation. Status code: {response.StatusCode}");
            }
        }

       
    }
}
