using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationProject.Models;

namespace UnitTestingGraduationProject
{
    public static class TestDataHelper
    {
        public static List<Beverage> GetFakeBeverageListFromDatabase()
        {
            return new List<Beverage>()
            {

                new Beverage
                {
                    BeverageId = 1,
                    Name = "Strawberry Lemonade",
                    Tag = "A refreshing drink perfect for summer",
                    Alcohol = false,
                    Glass = GlassType.CopperMug,
                    Instruction = "Mix together lemon juice, water, and sugar in a pitcher. Add sliced strawberries and stir. Serve over ice.",
                    Image = "https://www.example.com/strawberry-lemonade.jpg",
                    ImageAttribution = "Photo by John Smith",
                    CreativeCommonsConfirmed = true,
                    BeverageIngredients = new List<BeverageIngredient>()
                    {
                        new BeverageIngredient(){
                            Ingredient = new Ingredient
                            {
                                Name = "Lemon",
                                Description = "A citrus fruit with sour juice.",
                                Image = "https://example.com/lemon.jpg"
                            },
                            Measurement = "1/2 oz",
                            }
                    },
                    Source = BeverageSource.Local,
                },
                 new Beverage
                    {
                        BeverageId = 2,
                        Name = "Iced Tea",
                        Tag = "A refreshing summer beverage",
                        Alcohol = false,
                        Glass = GlassType.CopperMug,
                        Instruction = "Brew tea and let cool. Add sugar and lemon juice, if desired. Serve over ice.",
                        Image = "https://www.example.com/iced-tea.jpg",
                        ImageAttribution = "Photo by John Smith",
                        CreativeCommonsConfirmed = true,
                        BeverageIngredients = new List<BeverageIngredient>()
                        {
                            new BeverageIngredient(){
                                Ingredient = new Ingredient
                                {
                                    Name = "Black Tea",
                                    Description = "A type of tea that is more oxidized than green, white, and oolong teas.",
                                    Image = "https://example.com/black-tea.jpg"
                                },
                                Measurement = "8 oz",
                            }
                        },
                        Source = BeverageSource.Local
                    },
                new Beverage
                    {
                        BeverageId = 3,
                        Name = "Margarita",
                        Tag = "A classic tequila cocktail",
                        Alcohol = true,
                        Glass = GlassType.MargaritaGlass,
                        Instruction = "Combine tequila, lime juice, and Cointreau in a shaker with ice. Shake and strain into a salt-rimmed glass filled with ice.",
                        Image = "https://www.example.com/margarita.jpg",
                        ImageAttribution = "Photo by Jane Doe",
                        CreativeCommonsConfirmed = true,
                        BeverageIngredients = new List<BeverageIngredient>()
                        {
                            new BeverageIngredient(){
                                Ingredient = new Ingredient
                                {
                                    Name = "Tequila",
                                    Description = "A distilled spirit made from the blue agave plant.",
                                    Image = "https://example.com/tequila.jpg"
                                },
                                Measurement = "2 oz",
                            }
                        },
                        Source = BeverageSource.Local
                    },
            };
        }

        public static Task<List<Beverage>> GetFakeListBeveragesFromApi()
        {
            List<Beverage> beverages = new(){
                new Beverage
                    {
                        BeverageId = 4,
                        Name = "Mojito",
                        Tag = "A refreshing rum cocktail",
                        Alcohol = true,
                        Glass = GlassType.HighballGlass,
                        Instruction = "Muddle mint leaves and simple syrup in a glass. Add lime juice, rum, and ice. Top with club soda and stir.",
                        Image = "https://www.example.com/mojito.jpg",
                        ImageAttribution = "Photo by Jane Doe",
                        CreativeCommonsConfirmed = true,
                        BeverageIngredients = new List<BeverageIngredient>()
                        {
                            new BeverageIngredient(){
                                Ingredient = new Ingredient
                                {
                                    Name = "Mint",
                                    Description = "A herb with a refreshing, cool taste.",
                                    Image = "https://example.com/mint.jpg"
                                },
                                Measurement = "10 leaves",
                            }
                        },
                        Source = BeverageSource.CocktailDB
                    },
                new Beverage
                    {
                        BeverageId = 3,
                        Name = "Tomata Margarita",
                        Tag = "A classic tequila cocktail",
                        Alcohol = true,
                        Glass = GlassType.MartiniGlass,
                        Instruction = "Combine tequila, lime juice, and Cointreau in a shaker with ice. Shake and strain into a salt-rimmed glass filled with ice.",
                        Image = "https://www.example.com/margarita.jpg",
                        ImageAttribution = "Photo by Jane Doe",
                        CreativeCommonsConfirmed = true,
                        BeverageIngredients = new List<BeverageIngredient>()
                        {
                            new BeverageIngredient(){
                                Ingredient = new Ingredient
                                {
                                    Name = "Tomato",
                                    Description = "A distilled spirit made from the blue agave plant.",
                                    Image = "https://example.com/tequila.jpg"
                                },
                                Measurement = "2 oz",
                            }
                        },
                        Source = BeverageSource.CocktailDB
                },
            };
            return Task.FromResult(beverages);
        }

        public static Task<List<Beverage>> GetFakeListWithNoBeveragesFromApi()
        {
            List<Beverage> beverages = new();
            return Task.FromResult(beverages);
        }
    }
}
