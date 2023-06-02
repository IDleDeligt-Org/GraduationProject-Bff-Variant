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

        [HttpGet("local/all")]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _context.Ingredients.ToListAsync();
            return Ok(ingredients);
        }
    }
}
