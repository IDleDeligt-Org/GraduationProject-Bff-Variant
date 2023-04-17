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
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICocktailDBApi _cocktail;
        private readonly ApplicationDbContext _context;

        public BeverageController(IApplicationDbContext context, ICocktailDBApi cocktailDBApi)
        {
            _applicationDbContext = context;
            _cocktail = cocktailDBApi;
            _context = (ApplicationDbContext)context;
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

        [HttpGet("/start")]
        public async Task<IActionResult> GetRandomStartBeverages()
        {
            Random randomNumber = new Random();
            var ginTask = _cocktail.GetBeveragesByIngredient("gin");
            var rumTask = _cocktail.GetBeveragesByIngredient("rum");
            var vodkaTask = _cocktail.GetBeveragesByIngredient("vodka");
            var tequilaTask = _cocktail.GetBeveragesByIngredient("tequila");
            var whiskeyTask = _cocktail.GetBeveragesByIngredient("whiskey");
            var nonAlcoholicTask = _cocktail.GetAllNonAlcoholicDrinks();

            await Task.WhenAll(ginTask, rumTask, vodkaTask, tequilaTask, whiskeyTask, nonAlcoholicTask);

            List<Beverage>? ginResults = await ginTask;
            List<Beverage>? rumResults = await rumTask;
            List<Beverage>? vodkaResults = await vodkaTask;
            List<Beverage>? tequilaResults = await tequilaTask;
            List<Beverage>? whiskeyResults = await whiskeyTask;
            List<Beverage>? nonAlcoholicResults = await nonAlcoholicTask;



            List<Beverage> randomResults = new()
            {
                ginResults[randomNumber.Next(0, ginResults.Count-1)],
                rumResults[randomNumber.Next(0,rumResults.Count-1)],
                vodkaResults[randomNumber.Next(0, vodkaResults.Count - 1)],
                tequilaResults[randomNumber.Next(0, vodkaResults.Count - 1)],
                whiskeyResults[randomNumber.Next(0,whiskeyResults.Count - 1)],
                nonAlcoholicResults[randomNumber.Next(0, nonAlcoholicResults.Count - 1)]
            };

            return Ok(randomResults);
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

