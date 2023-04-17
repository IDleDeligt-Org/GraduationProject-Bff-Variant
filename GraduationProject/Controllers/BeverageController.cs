using System.Collections.Generic;
using System.Linq;
using GraduationProject.CocktailDB;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/beverage")]
    [ApiController]
    public class BeverageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICocktailDBApi _cocktail;

        public BeverageController(ApplicationDbContext context, ICocktailDBApi cocktailDBApi)
        {
            _context = context;
            _cocktail = cocktailDBApi;
        }

        [HttpGet("{search}")]
        public async Task<IActionResult> GetBeveragesByName(string search)
        {
            // Query local database
            var localResults = await _context.Beverages
                .Where(b => b.Name.Contains(search))
                .Include(b => b.BeverageIngredients)
                .ThenInclude(bi => bi.Ingredient)
                .ToListAsync();

            // Query third-party API
            var apiResults = await _cocktail.GetBeveragesByName(search);

            if (localResults == null)
            {
                return Ok(apiResults);
            }
            // Combine and return results
            var results = localResults.Concat(apiResults).ToList();

            return Ok(results);
        }

        

        //[HttpGet("{name}")]
        //public async Task<IActionResult> GetBeverages(string name)
        //{
        //    IEnumerable<Beverage> beverages = await _context.Beverages.Include(b => b.BeverageIngredients).ThenInclude(bi => bi.Ingredient).ToListAsync();

        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        beverages = _context.Beverages.Where(b => b.Name.Contains(name));
        //    }

        //    return Ok(beverages);
        //}

        //[HttpGet("id/{id}")]
        //public async Task<IActionResult> GetBeverage(int id)
        //{
        //    IEnumerable<Beverage> beverages = await _context.Beverages.ToListAsync();

        //    return Ok(beverages);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBeverage(int id, Beverage beverage)
        //{
        //    if (id != beverage.BeverageId)
        //    {
        //        return BadRequest();
        //    }

        //    Beverage? beverageToUpdate = await _context.Beverages
        //        .Include(b => b.BeverageIngredients)
        //        .SingleOrDefaultAsync(b => b.BeverageId == id);

        //    if (beverageToUpdate == null)
        //    {
        //        return NotFound();
        //    }

        //    // Update the beverage properties
        //    beverageToUpdate.Name = beverage.Name;
        //    beverageToUpdate.Tag = beverage.Tag;
        //    beverageToUpdate.Alcohol = beverage.Alcohol;
        //    beverageToUpdate.Instruction = beverage.Instruction;
        //    beverageToUpdate.Image = beverage.Image;
        //    beverageToUpdate.Video = beverage.Video;

        //    // Update the beverage ingredients
        //    List<BeverageIngredient>? newBeverageIngredients = new List<BeverageIngredient>();
        //    foreach (BeverageIngredient beverageIngredient in beverage.BeverageIngredients)
        //    {
        //        BeverageIngredient? existingBeverageIngredient = beverageToUpdate.BeverageIngredients.SingleOrDefault(bi => bi.IngredientId == beverageIngredient.IngredientId);
        //        if (existingBeverageIngredient != null)
        //        {
        //            existingBeverageIngredient.Measurment = beverageIngredient.Measurment;
        //        }
        //        else
        //        {
        //            newBeverageIngredients.Add(beverageIngredient);
        //        }
        //    }

        //    //beverageToUpdate.BeverageIngredients.AddRange(newBeverageIngredients);

        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        //[HttpPost]
        //public async Task<ActionResult<Beverage>> PostBeverage(Beverage beverage)
        //{
        //    _context.Beverages.Add(beverage);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetBeverage), new { id = beverage.BeverageId }, beverage);
        //}

        //[HttpDelete("{name}")]
        //public async Task<IActionResult> DeleteBeverage(string name)
        //{
        //    Beverage? beverage = await _context.Beverages
        //        .Include(b => b.BeverageIngredients)
        //        .SingleOrDefaultAsync(b => b.Name == name);

        //    if (beverage == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Beverages.Remove(beverage);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

    }
}

