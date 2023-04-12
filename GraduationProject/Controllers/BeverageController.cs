using System.Collections.Generic;
using System.Linq;
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

        public BeverageController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Beverage>> GetBeverages()
        //{
        //    return _context.Beverages.ToList();
        //}

        [HttpGet("{name}")]
        public async Task<IActionResult> GetBeverages(string name)
        {
            IEnumerable<Beverage> beverages = await _context.Beverages.Include(b => b.BeverageIngredients).ThenInclude(bi => bi.Ingredient).ToListAsync();

            if (!string.IsNullOrEmpty(name))
            {
                beverages = _context.Beverages.Where(b => b.Name.Contains(name));
            }

            return Ok(beverages); 
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetBeverage(int id)
        {
            //IQueryable<Beverage> beverages = _context.Beverages.Include(b => b.BeverageIngredients).ThenInclude(bi => bi.Ingredient);
            IEnumerable<Beverage> beverages = await _context.Beverages.ToListAsync();

            return Ok(beverages);
        }

        //[HttpPut("{id}")]
        //public IActionResult PutBeverage(int id, Beverage beverage)
        //{
        //    if (id != beverage.BeverageId)
        //    {
        //        return BadRequest();
        //    }

        //    var beverageToUpdate = _context.Beverages
        //        .Include(b => b.BeverageIngredients)
        //        .SingleOrDefault(b => b.BeverageId == id);

        //    if (beverageToUpdate == null)
        //    {
        //        return NotFound();
        //    }

        //    // Update the beverage properties
        //    beverageToUpdate.Name = beverage.Name;
        //    beverageToUpdate.Description = beverage.Description;
        //    beverageToUpdate.ImageUrl = beverage.ImageUrl;

        //    // Update the beverage ingredients
        //    var newBeverageIngredients = new List<BeverageIngredient>();
        //    foreach (var beverageIngredient in beverage.BeverageIngredients)
        //    {
        //        var existingBeverageIngredient = beverageToUpdate.BeverageIngredients.SingleOrDefault(bi => bi.IngredientId == beverageIngredient.IngredientId);
        //        if (existingBeverageIngredient != null)
        //        {
        //            existingBeverageIngredient.Quantity = beverageIngredient.Quantity;
        //        }
        //        else
        //        {
        //            newBeverageIngredients.Add(beverageIngredient);
        //        }
        //    }

        //    beverageToUpdate.BeverageIngredients.AddRange(newBeverageIngredients);

        //    _context.SaveChanges();

        //    return NoContent();
        //}


        [HttpPost]
        public ActionResult<Beverage> PostBeverage(Beverage beverage)
        {
            _context.Beverages.Add(beverage);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBeverage), new { id = beverage.BeverageId }, beverage);
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteBeverage(string name)
        {
            Beverage? beverage = _context.Beverages.Find(name);
            if (beverage == null)
            {
                return NotFound();
            }

            _context.Beverages.Remove(beverage);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

