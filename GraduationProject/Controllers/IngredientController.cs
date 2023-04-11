// Controllers/IngredientController.cs
using System.Collections.Generic;
using System.Linq;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IngredientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ingredient>> GetIngredients()
        {
            return _context.Ingredients.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Ingredient> GetIngredient(int id)
        {
            var ingredient = _context.Ingredients.Find(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }

        [HttpPut("{id}")]
        public IActionResult PutIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(ingredient).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Ingredient> PostIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetIngredient), new { id = ingredient.IngredientId }, ingredient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIngredient(int id)
        {
            var ingredient = _context.Ingredients.Find(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
