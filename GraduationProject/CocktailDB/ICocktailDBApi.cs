using GraduationProject.Models;
using GraduationProject.Models.CocktailDB;

namespace GraduationProject.CocktailDB
{
    public interface ICocktailDBApi
    {
        Task<List<Drink>> GetDrinks(string search);
    }
}
