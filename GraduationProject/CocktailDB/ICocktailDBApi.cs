using GraduationProject.Models;
using GraduationProject.Models.CocktailDB;

namespace GraduationProject.CocktailDB
{
    public interface ICocktailDBApi
    {
        Task<List<Beverage>> GetBeverages(string search);
    }
}
