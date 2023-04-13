// Controllers/UserController.cs
using System.Collections.Generic;
using System.Linq;
using GraduationProject.CocktailDB;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/Favorites")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICocktailDBApi _cocktail;

        public FavoriteController(ApplicationDbContext context, ICocktailDBApi cocktailDBApi)
        {
            _context = context;
            _cocktail = cocktailDBApi;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Beverage>>> GetUserFavorites(int userId)
        {
            User? user = _context.Users.Find(userId);

            if (user == null)
            {
                return NotFound();
            }
            List<Beverage>? userFavorites = await _context.Favorites
                .Include(f => f.Beverage)
                .ThenInclude(b => b.BeverageIngredients)
                .ThenInclude(bi => bi.Ingredient)
                .Where(f => f.UserId == userId)
                .Select(f => f.Beverage)
                .ToListAsync();

            if (userFavorites == null)
            {
                return NotFound();
            }

            // Fetch external beverages
            List<Beverage>? externalBeverages = new List<Beverage>();
            foreach (var beverage in userFavorites)
            {
                if (beverage.Source == BeverageSource.CocktailDB)
                {
                    Beverage? externalBeverage = await _cocktail.GetBeverageById(beverage.BeverageId);
                    externalBeverages.Add(externalBeverage);
                }
            }

            // Combine local and external beverages
            List<Beverage>? combinedFavorites = userFavorites
                .Where(b => b.Source == BeverageSource.Local)
                .Concat(externalBeverages)
                .ToList();

            return combinedFavorites;
        }
        
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
