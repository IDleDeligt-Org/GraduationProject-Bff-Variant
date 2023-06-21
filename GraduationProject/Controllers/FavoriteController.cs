// Controllers/UserController.cs
using System.Collections.Generic;
using System.Linq;
using GraduationProject.CocktailDB;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/favorite")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly ICocktailDBApi _cocktail;
        private readonly IApplicationDbContext _context;

        public FavoriteController(IApplicationDbContext context, ICocktailDBApi cocktailDBApi)
        {
            _cocktail = cocktailDBApi;
            _context = context;
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

           
            List<Beverage> fullListFavorites = new();

            foreach (Favorite favorite in userFavorites)
            {
                if (favorite.Source == BeverageSource.Local)
                {
                    Beverage? localBeverage = await _context.Beverages
                    .Include(b => b.BeverageIngredients)
                    .ThenInclude(bi => bi.Ingredient)
                    .FirstOrDefaultAsync(b => b.BeverageId == favorite.FavoriteBeverageId);
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

        [HttpPost("user/{userId}")]
        public async Task<ActionResult<FavoriteDto>> PostFavorite(int userId, FavoriteDto favorite)
        {
            if (favorite.Source == BeverageSource.CocktailDB || favorite.Source == BeverageSource.Local)
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                if (await _context.Favorites.AnyAsync(f => f.FavoriteBeverageId == favorite.FavoriteBeverageId))
                {
                    return Ok("This favorite already exists.");
                }

                var newFavorite = new Favorite{
                    FavoriteBeverageId = favorite.FavoriteBeverageId,
                    Source = favorite.Source,
                    User = user,
                    UserId = userId
                };

                _context.Favorites.Add(newFavorite);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUserFavorites), new { userId = favorite.UserId }, favorite);
            }
            else
            {
                return BadRequest("Invalid beverage source.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            Favorite? favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.FavoriteBeverageId == id);

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
