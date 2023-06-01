using System.Collections.Generic;
using System.Linq;
using GraduationProject.CocktailDB;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading.Tasks;
using GraduationProject.Models.CocktailDB;


namespace GraduationProject.Controllers
{
    [Route("api/beverage")] 
    [ApiController]
    public class BeverageController : ControllerBase
    {
        private readonly ICocktailDBApi _cocktailDbApi;
        private readonly IApplicationDbContext _context;

        public BeverageController(IApplicationDbContext context, ICocktailDBApi cocktailDBApi)
        {
            _cocktailDbApi = cocktailDBApi;
            _context = context;
        }

        [HttpGet("name/{search}")]
        public async Task<IActionResult> GetBeveragesByName( string search )
        {
            // Get cocktails from the API
            List<Beverage>? apiResponses = await _cocktailDbApi.GetBeveragesByName(search);

            // Get IDs of the cocktails from the API results
            var apiIds = apiResponses.Select(b => b.ApiId).ToList();

            // Get cocktails from the local database
            var localResults = await _context.Beverages
                .Where(b => apiIds.Contains(b.ApiId))
                .Include(b => b.BeverageIngredients)
                .ThenInclude(bi => bi.Ingredient)
                .ToListAsync();

            // Get IDs of the cocktails in the local database
            var localApiIds = localResults.Select(b => b.ApiId).ToList();

            // Find cocktails that are in the API results but not in the local database
            var newCocktails = apiResponses.Where(b => !localApiIds.Contains(b.ApiId)).ToList();

            // Add new cocktails to the local database
            foreach (var newCocktail in newCocktails) {
                _context.Beverages.Add(newCocktail);
            }

            await _context.SaveChangesAsync();

            // Combine local and new results
            var allCocktails = localResults.Concat(newCocktails).ToList();

            return Ok(allCocktails);
        }

        [HttpGet("ingredient/{search}")]
        public async Task<IActionResult> GetBeveragesByIngredient( string search )
        {
            // Get beverages from the API
            List<Beverage>? apiResponses = await _cocktailDbApi.GetBeveragesByIngredient(search);

            // Get IDs of the beverages from the API results
            var apiIds = apiResponses.Select(b => b.ApiId).ToList();

            var localResults = await _context.Beverages
                .Where(b => b.BeverageIngredients
                .Any(bi => bi.Ingredient.Name.Contains(search)))
                .Include(b => b.BeverageIngredients)
                .ThenInclude(bi => bi.Ingredient)
                .ToListAsync();

            // Get IDs of the beverages in the local database
            var localApiIds = localResults.Select(b => b.ApiId).ToList();

            // Find beverages that are in the API results but not in the local database
            var newBeverages = apiResponses.Where(b => !localApiIds.Contains(b.ApiId)).ToList();

            // Add new beverages to the local database
            foreach (var newBeverage in newBeverages) {
                _context.Beverages.Add(newBeverage);
            }

            await _context.SaveChangesAsync();

            // Combine local and new results
            var allBeverages = localResults.Concat(newBeverages).ToList();

            return Ok(allBeverages);
        }

        [HttpGet("search/non_alcoholic")]
        public async Task<IActionResult> GetBeveragesNonAlcoholic()
        {
            // Get non-alcoholic beverages from the API
            List<Beverage>? apiResponses = await _cocktailDbApi.GetAllNonAlcoholicDrinks();

            // Get IDs of the beverages from the API results
            var apiIds = apiResponses.Select(b => b.ApiId).ToList();

            // Get beverages from the local database
            var localResults = await _context.Beverages
                .Where(b => apiIds.Contains(b.ApiId))
                .Include(b => b.BeverageIngredients)
                .ThenInclude(bi => bi.Ingredient)
                .ToListAsync();

            // Get IDs of the beverages in the local database
            var localApiIds = localResults.Select(b => b.ApiId).ToList();

            // Find beverages that are in the API results but not in the local database
            var newBeverages = apiResponses.Where(b => !localApiIds.Contains(b.ApiId)).ToList();

            // Add new beverages to the local database
            foreach (var newBeverage in newBeverages) {
                _context.Beverages.Add(newBeverage);
            }

            await _context.SaveChangesAsync();

            // Combine local and new results
            var allBeverages = localResults.Concat(newBeverages).ToList();

            return Ok(allBeverages);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetBeverage(int id)
        {
            IEnumerable<Beverage> beverages = await _context.Beverages.ToListAsync();

            return Ok(beverages);
        }

        [HttpPost]
        public async Task<ActionResult<BeverageDto>> PostBeverage(BeverageDto beverageDto)
        {
            // Check if a beverage with the same name already exists in the local database
            if (await _context.Beverages.AnyAsync(b => b.Name == beverageDto.Name))
            {
                return Conflict("A beverage with the same name already exists.");
            }
            try
            {
                // Map BeverageDto to Beverage
                var beverage = new Beverage
                {
                    Name = beverageDto.Name,
                    Tag = beverageDto.Tag,
                    Alcohol = beverageDto.Alcohol,
                    Glass = (GlassType)beverageDto.Glass,
                    Instruction = beverageDto.Instruction,
                    Image = beverageDto.Image,
                    Video = beverageDto.Video,
                    ImageAttribution = beverageDto.ImageAttribution,
                    CreativeCommonsConfirmed = beverageDto.CreativeCommonsConfirmed
                };
                _context.Beverages.Add(beverage);
                if (beverageDto.BeverageIngredients != null)
                {
                    foreach (BeverageIngredientDto beverageIngredientDto in beverageDto.BeverageIngredients)
                    {
                        // Find the Ingredient with the given IngredientId
                        Ingredient? ingredient = await _context.Ingredients.FindAsync(beverageIngredientDto.IngredientId);

                        if (ingredient == null)
                        {
                            // Create a new Ingredient if it's not found
                            var ingredientCreationDto = beverageIngredientDto.Ingredient;
                            ingredient = new Ingredient
                            {
                                
                                Name = ingredientCreationDto.Name,
                                Description = ingredientCreationDto.Description,
                                Image = ingredientCreationDto.Image
                            };

                            // Add the new Ingredient to the local database
                            _context.Ingredients.Add(ingredient);
                        }

                        // Map BeverageIngredientDto to BeverageIngredient
                        BeverageIngredient? beverageIngredient = new BeverageIngredient
                        {
                            Beverage = beverage,
                            Ingredient = ingredient,
                            Measurement = beverageIngredientDto.Measurement
                        };

                        _context.BeverageIngredients.Add(beverageIngredient);
                    }
                }

                    await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBeverage), new { id = beverage.BeverageId }, beverage);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while saving the entity changes: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeverage(int id)
        {
            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage == null)
            {
                return NotFound();
            }

            _context.Beverages.Remove(beverage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeverage(int id, BeverageDto beverageDto)
        {
            if (id != beverageDto.BeverageId)
            {
                return BadRequest();
            }

            try
            {
                var beverage = await _context.Beverages
                    .Include(b => b.BeverageIngredients)
                    .SingleOrDefaultAsync(b => b.BeverageId == id);

                if (beverage == null)
                {
                    return NotFound();
                }

                // Update Beverage properties
                beverage.Name = beverageDto.Name;
                beverage.Tag = beverageDto.Tag;
                beverage.Alcohol = beverageDto.Alcohol;
                beverage.Glass = (GlassType)beverageDto.Glass;
                beverage.Instruction = beverageDto.Instruction;
                beverage.Image = beverageDto.Image;
                beverage.Video = beverageDto.Video;
                beverage.ImageAttribution = beverageDto.ImageAttribution;
                beverage.CreativeCommonsConfirmed = beverageDto.CreativeCommonsConfirmed;

                // Update BeverageIngredient properties
                if (beverageDto.BeverageIngredients != null)
                {
                    foreach (BeverageIngredientDto beverageIngredientDto in beverageDto.BeverageIngredients)
                    {
                        // Find the Ingredient with the given IngredientId
                        Ingredient? ingredient = await _context.Ingredients.FindAsync(beverageIngredientDto.IngredientId);

                        if (ingredient == null)
                        {
                            // Create a new Ingredient if it's not found
                            var ingredientCreationDto = beverageIngredientDto.Ingredient;
                            ingredient = new Ingredient
                            {
                                
                                Name = ingredientCreationDto.Name,
                                Description = ingredientCreationDto.Description,
                                Image = ingredientCreationDto.Image
                            };

                            // Add the new Ingredient to the local database
                            _context.Ingredients.Add(ingredient);
                        }

                        // Find the existing BeverageIngredient
                        BeverageIngredient? existingBeverageIngredient = beverage.BeverageIngredients
                            .FirstOrDefault(bi => bi.BeverageId == beverage.BeverageId && bi.IngredientId == ingredient.IngredientId);

                        if (existingBeverageIngredient != null)
                        {
                            // Update the existing BeverageIngredient
                            existingBeverageIngredient.Measurement = beverageIngredientDto.Measurement;
                        }
                        else
                        {
                            // Create a new BeverageIngredient
                            BeverageIngredient? beverageIngredient = new BeverageIngredient
                            {
                                Beverage = beverage,
                                Ingredient = ingredient,
                                Measurement = beverageIngredientDto.Measurement
                            };

                            _context.BeverageIngredients.Add(beverageIngredient);
                        }
                    }
                }

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
