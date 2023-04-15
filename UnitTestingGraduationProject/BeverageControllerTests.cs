using GraduationProject;
using GraduationProject.CocktailDB;
using GraduationProject.Controllers;
using GraduationProject.Models;
using GraduationProject.Models.CocktailDB;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;

namespace UnitTestingGraduationProject
{
    public class BeverageControllerTests
    {
        private Mock<ApplicationDbContext> _mockDbContext = new Mock<ApplicationDbContext>();
        private List<Beverage> _localResults;
        private BeverageController _controller;
        private Mock<ICocktailDBApi> _mockCocktailApi;
        private StringContent _apiResults;


        public BeverageControllerTests()
        {
            _mockCocktailApi = new Mock<ICocktailDBApi>();
            _controller = new BeverageController(_mockDbContext.Object, _mockCocktailApi.Object);
            _mockDbContext = new Mock<ApplicationDbContext>();

            _localResults = new List<Beverage>()
            {
                new Beverage() {
                            BeverageId = 1,
                            Alcohol = true,
                            CreativeCommonsConfirmed = false,
                            Glass = "Martini Glass",
                            Image = "http://potatomargarita.com",
                            Instruction = "Shake it like a polaroid picture",
                            Name = "Potato Margarita",
                            Tag = "ordinary"
                        },
                new Beverage() {
                            BeverageId = 2,
                            Alcohol = true,
                            CreativeCommonsConfirmed = false,
                            Glass = "Thumbler",
                            Image = "http://tomatomartini.com",
                            Instruction = "Stir it up",
                            Name = "Tomato Martini",
                            Tag = "cocktail"
                        },
                new Beverage() {
                            BeverageId = 3,
                            Alcohol = false,
                            CreativeCommonsConfirmed = false,
                            Glass = "Long glass",
                            Image = "http://brocolioldfashined.com",
                            Instruction = "On the grind",
                            Name = "Brocoli Old Fashioned",
                            Tag = "ordinary"
                        }
            };


            _apiResults = new StringContent(JsonConvert.SerializeObject(new BeveragesApiResponse()
            {
                drinks = new List<BeverageApiResponse>()
            {
                new BeverageApiResponse() {
                    idDrink = 11728,
                    strDrink = "Martini",
                    strCategory = "Ordinary Drink",
                    strAlcoholic = "Alcoholic",
                    strGlass = "Cocktail",
                    strInstructions = "Shake it all about",
                    strDrinkThumb = "http://tomatomartini.com",
                    strImageAttribution = null,
                    strCreativeCommonsConfirmed = "yes"},
                new BeverageApiResponse() {
                    idDrink = 11005,
                    strDrink = "Dry Martini",
                    strCategory = "Cocktail",
                    strAlcoholic = "Optional Alcoholic",
                    strGlass = "Unknown",
                    strVideo = null,
                    strInstructions = "Do the shake",
                    strDrinkThumb = "http://tomatomartini.com",
                    strCreativeCommonsConfirmed = "yes"
                },
                new BeverageApiResponse() {
                    idDrink = 11007,
                    strDrink = "Margarita",
                    strCategory = "Sour",
                    strAlcoholic = "None",
                    strGlass = "Cocktail",
                    strInstructions = "Smash them berries",
                    strDrinkThumb = "http://testing.com",
                    strImageAttribution = null,
                    strCreativeCommonsConfirmed = "no"},
                }
            }));
        }
        //_mockCocktailApi.Setup(s => s.GetBeverages(search)).ReturnsAsync();
        //_mockDbContext.Setup(c => c.Beverages.Where(b => b.Name.Contains(search)).Include(b => b.BeverageIngredients).ThenInclude(bi => bi.Ingredient).ToListAsync()).ReturnsAsync(_localResults);
    }
}
