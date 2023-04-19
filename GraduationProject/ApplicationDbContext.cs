using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GraduationProject
{
    public interface IApplicationDbContext
    {

        DbSet<User> Users { get; set; }
        DbSet<Beverage> Beverages { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<Favorite> Favorites { get; set; }
        DbSet<BeverageIngredient> BeverageIngredients { get; set; }

        Task<int> SaveChangesAsync();
    }

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<BeverageIngredient> BeverageIngredients { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                 new User { UserId = 1, UserName = "ChuckNorris" },
                 new User { UserId = 2, UserName = "BruceLee" }
             );

            modelBuilder.Entity<Favorite>().HasData(
                new Favorite { FavoriteId = 1, UserId = 1, Source = BeverageSource.Local, FavoriteBeverageId = 1 },
                new Favorite { FavoriteId = 2, UserId = 2, Source = BeverageSource.Local, FavoriteBeverageId = 2 },
                new Favorite { FavoriteId = 3, UserId = 2, Source = BeverageSource.CocktailDB,  FavoriteBeverageId = 11000 } 
            );

            modelBuilder.Entity<Beverage>().HasData(
                new Beverage { BeverageId = 1, Name = "Potato Margarita", Tag = "ordinary", Alcohol = true, Instruction = "Shake it like a polaroid picture", Glass = GlassType.MartiniGlass, Image = "http://potatomargarita.com" },
                new Beverage { BeverageId = 2, Name = "Tomato Martini", Tag = "cocktail", Alcohol = true, Instruction = "Stir it up", Glass = GlassType.MasonJar, Image = "http://tomatomartini.com" },
                new Beverage { BeverageId = 3, Name = "Brocoli Old Fashioned", Tag = "ordinary", Alcohol = false, Instruction = "On the grind", Glass = GlassType.CoupeGlass, Image = "http://brocolioldfashined.com" }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { IngredientId = 1, Name = "Brocoli Liqueur", Description = "Great vegetable, quite bitter", Image = "http://brocoli.com" },
                new Ingredient { IngredientId = 2, Name = "Potato", Description = "Saved nations from famine", Image = "http://potato.com" },
                new Ingredient { IngredientId = 3, Name = "Tomato extract", Description = "The italian berry", Image = "http://tomato.com" }
            );

            modelBuilder.Entity<BeverageIngredient>().HasData(
                new BeverageIngredient { BeverageIngredientId = 1, BeverageId = 1, IngredientId = 1, Measurement = "60ml" },
                new BeverageIngredient { BeverageIngredientId = 2, BeverageId = 1, IngredientId = 2, Measurement = "One Slice" },
                new BeverageIngredient { BeverageIngredientId = 3, BeverageId = 1, IngredientId = 3, Measurement = "35ml" },
                new BeverageIngredient { BeverageIngredientId = 4, BeverageId = 2, IngredientId = 2, Measurement = "One Slice" },
                new BeverageIngredient { BeverageIngredientId = 5, BeverageId = 2, IngredientId = 3, Measurement = "35ml" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
