// Controllers/UserController.cs
using System.Collections.Generic;
using System.Linq;
using GraduationProject.CocktailDB;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/Favorite")]
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
            User? user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            List<Favorite>? userFavorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .ToListAsync();

            if (userFavorites == null)
            {
                return NotFound();
            }

           
            List<Beverage> fullListFavorites = new List<Beverage>();

            foreach (Favorite favorite in userFavorites)
            {
                if (favorite.Source == BeverageSource.Local)
                {
                    Beverage? localBeverage = await _context.Beverages.FindAsync(favorite.FavoriteBeverageId);
                    if (localBeverage != null)
                    {
                        fullListFavorites.Add(localBeverage);
                    }
                }
                else if (favorite.Source == BeverageSource.CocktailDB)
                {
                    Beverage? externalBeverage = await _cocktail.GetBeverageById(favorite.FavoriteBeverageId);
                    if (externalBeverage != null)
                    {
                        fullListFavorites.Add(externalBeverage);
                    }
                }
            }

            return fullListFavorites;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            User? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpPost]
        public async Task<ActionResult<Favorite>> PostFavorite(Favorite favorite)
        {
            if (favorite.Source == BeverageSource.CocktailDB || favorite.Source == BeverageSource.Local)
            
            { _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserFavorites), new { userId = favorite.UserId }, favorite);
            }
        else
    
        return BadRequest("Invalid beverage source.");
        }


    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            Favorite? favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
