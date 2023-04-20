// Controllers/IngredientController.cs
using System.Collections.Generic;
using System.Linq;
using GraduationProject.CocktailDB;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{ 
    [Route("api/ingredient")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly ICocktailDBApi _cocktail;
        private readonly IApplicationDbContext _context;

        public IngredientController(IApplicationDbContext context, ICocktailDBApi cocktailDBApi)
        {
            _cocktail = cocktailDBApi;
            _context = context;
        }

        [HttpGet("{search}")]
        public async Task<IActionResult> GetBeveragesByIngredient(string search)
        {
            var localResults = await _context.Beverages
                .Where(b => b.BeverageIngredients
                .Any(bi => bi.Ingredient.Name.Contains(search)))
                .Include(b => b.BeverageIngredients)
                .ThenInclude(bi => bi.Ingredient)
                .ToListAsync();

            List<Beverage> apiResults = await _cocktail.GetBeveragesByIngredient(search);

            if (localResults == null)
            {
                return Ok(apiResults);
            }
            // Combine and return results
            var results = localResults.Concat(apiResults).ToList();

            if(results.Count == 0) {
                return Ok(new List<Beverage>());
            }

            return Ok(results);
        }

        [HttpGet("search/non_alcoholic")]
        public async Task<IActionResult> GetBeveragesNonAlcoholic()
        {
            var localResults = await _context.Beverages
                .Where(b => b.Alcohol == false)
                .Include(b => b.BeverageIngredients)
                .ThenInclude(bi => bi.Ingredient)
                .ToListAsync();

            List<Beverage> apiResults = await _cocktail.GetAllNonAlcoholicDrinks();

            if (localResults == null)
            {
                return Ok(apiResults);
            }
            // Combine and return results
            var results = localResults.Concat(apiResults).ToList();

            if (results.Count == 0)
            {
                return NotFound();
            }

            return Ok(results);
        }
        
        [HttpGet("local/all")]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _context.Ingredients.ToListAsync();
            return Ok(ingredients);
        }
    }
}
