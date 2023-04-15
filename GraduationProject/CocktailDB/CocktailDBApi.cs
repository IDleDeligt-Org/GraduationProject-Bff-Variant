using GraduationProject.Models.CocktailDB;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;
using GraduationProject.Models;
using Microsoft.IdentityModel.Tokens;

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
                return BeverageApiResponseToBeverage(result!);
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


        public List<Beverage> BeverageApiResponseToBeverage(BeveragesApiResponse beveragesApiResponse)
        {
            List<Beverage> beverages = new List<Beverage>() { };

            if (beveragesApiResponse?.drinks != null)
            {
                foreach (BeverageApiResponse apiDrink in beveragesApiResponse?.drinks!)
                {
                    beverages.Add(DrinkMapper.DrinkToBeverage(apiDrink));
                }
            }
            return beverages;
        }
    }
}
