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
                DrinksApiResponse? newResult = await response.Content.ReadFromJsonAsync<DrinksApiResponse>();

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

        public async Task <List<Beverage>> GetBeverageById(int id)
        {
            HttpResponseMessage? response = await _httpClient.GetAsync($"api/json/v1/1/lookup.php?i={id}");

            if (response.IsSuccessStatusCode)
            {
                DrinksApiResponse? newResult = await response.Content.ReadFromJsonAsync<DrinksApiResponse>();

                List<Beverage> drinks = new List<Beverage>();

                if (newResult?.drinks != null)
                {

                    foreach (DrinkApiResponse apiDrink in newResult?.drinks!)
                    {
                        drinks.Add(DrinkMapper.DrinkToBeverage(apiDrink));
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
