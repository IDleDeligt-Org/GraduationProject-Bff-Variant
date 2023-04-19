using GraduationProject.Models;
using GraduationProject.Models.CocktailDB;

namespace GraduationProject.CocktailDB
{
    public interface ICocktailDBApi
    {
        Task<List<Beverage>> GetBeveragesByName(string search);
        Task<Beverage> GetBeverageById(int id);
        Task<List<Beverage>> GetBeveragesByIngredient(string search);
        Task<List<Beverage>> GetAllNonAlcoholicDrinks();

    }
}
