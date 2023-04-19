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
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;

namespace UnitTestingGraduationProject
{
    public class CocktailDbApiTests
    {
        private readonly StringContent _apiResultsSeveral;
        private readonly StringContent _apiResultsOne;
        private readonly Mock<HttpMessageHandler> _mockMessageHandler = new();
        private readonly HttpClient _httpClient;
        private readonly CocktailDBApi _api;

        public CocktailDbApiTests()
        {
            _httpClient= new HttpClient(_mockMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com")
            };

            _api = new CocktailDBApi(_httpClient);
      
            _apiResultsSeveral = new StringContent(JsonConvert.SerializeObject(new BeveragesApiResponse()
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
                }}));
            _apiResultsOne = new StringContent(JsonConvert.SerializeObject(new BeveragesApiResponse()
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
                    strCreativeCommonsConfirmed = "yes",
                    strIngredient1= "Gin",
                    strIngredient2= "Dry Vermouth",
                    strIngredient3 = "Olive",
                    strIngredient4 = null,
                    strIngredient5 = null,
                    strMeasure1 = "1 2/3 oz ",
                    strMeasure2 = "1/3 oz ",
                    strMeasure3 = "1 ",
                    strMeasure4 = null,
                    strMeasure5 = null,}
            }}));
        }
        
        [Fact]
        public async Task GetBeverages_TestCocktailDbApi_ValidResult_WithEntries()
        {
            // Arrange
            string search = "Margarita";
            _mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = _apiResultsSeveral
                });

            // Act
            var result = await _api.GetBeveragesByName(search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Margarita", result[2].Name);
            Assert.Equal(11007, result[2].BeverageId);
            Assert.Equal(GlassType.Unknown, result[0].Glass);
            Assert.Equal(GlassType.Unknown, result[1].Glass);
            Assert.True(result[0].Alcohol);
            Assert.True(result[1].Alcohol);
            Assert.False(result[2].Alcohol);
            Assert.Equal(BeverageSource.CocktailDB, result[0].Source);
            Assert.True(result[0].CreativeCommonsConfirmed);
            Assert.False(result[2].CreativeCommonsConfirmed);

        }

        [Fact]
        public async Task GetBeverages_TestCocktailDbApi_ValidResult_WithNoEntries()
        {
            // Arrange
            string search = "Margarita";
            _mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new BeveragesApiResponse()
                    {
                        drinks = null
                    }))
                 });

            //Act
            var result = await _api.GetBeveragesByName(search);

            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            
        }

        [Fact]
        public async Task GetBeverages_TestCocktailDbApi_InvalidResponse()
        {
            //Arrange
            string search = "Margarita";
            _mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                });

            //Act
            var caughtException = await Assert.ThrowsAsync<HttpRequestException>(async () => await _api.GetBeveragesByName(search));
            
            //Assert
            Assert.Equal("Failed to retrieve searchinformation. Status code: ServiceUnavailable", caughtException.Message);
        }

        [Fact]
        public async Task GetBeverageById_TestCocktailDbApi_ValidResponse_WithEntry()
        {
            int search = 11728;
            _mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = _apiResultsOne
                });
            Beverage beverage = await _api.GetBeverageById(search);

            Assert.NotNull(beverage);
            Assert.Equal(11728, beverage.BeverageId);
            Assert.Equal("Martini", beverage.Name);
            Assert.Equal("Ordinary Drink", beverage.Tag);
            Assert.True(beverage.Alcohol);
            Assert.Equal(GlassType.Unknown, beverage.Glass);
            Assert.Equal("Shake it all about", beverage.Instruction);
            Assert.Equal("http://tomatomartini.com", beverage.Image);
            Assert.Null(beverage.ImageAttribution);
            Assert.True(beverage.CreativeCommonsConfirmed);
            Assert.Equal(3, beverage.BeverageIngredients.Count);
        }

        [Fact]
        public async Task GetBeverageById_TestCocktailDbApi_ValidResponse_WithNoEntry()
        {
            int search = 11728;
            _mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new BeveragesApiResponse()
                    {
                        drinks = null
                    }))
                });
            Beverage beverage = await _api.GetBeverageById(search);

            Assert.NotNull(beverage);
            Assert.True(string.IsNullOrEmpty(beverage.Name));
            Assert.True(string.IsNullOrEmpty(beverage.Tag));
            Assert.Equal(GlassType.HighballGlass, beverage.Glass);
            Assert.True(string.IsNullOrEmpty(beverage.Instruction));
            Assert.True(string.IsNullOrEmpty(beverage.Image));
            Assert.Null(beverage.BeverageIngredients);
            Assert.False(beverage.Alcohol);
            Assert.False(beverage.CreativeCommonsConfirmed);
            Assert.True(string.IsNullOrEmpty(beverage.ImageAttribution));
        }
    }
}
