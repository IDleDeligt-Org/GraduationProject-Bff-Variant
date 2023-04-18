using GraduationProject;
using GraduationProject.CocktailDB;
using GraduationProject.Controllers;
using GraduationProject.Models;
using GraduationProject.Models.CocktailDB;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Moq.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UnitTestingGraduationProject
{
    public class BeverageControllerTests
    {
        private readonly Mock<IApplicationDbContext> _mockDbContext;
        private readonly BeverageController _controller;
        private readonly Mock<ICocktailDBApi> _mockCocktailApi;

        public BeverageControllerTests()
        {
            _mockCocktailApi = new Mock<ICocktailDBApi>();
            _mockDbContext = new Mock<IApplicationDbContext>();
            _controller = new BeverageController(_mockDbContext.Object, _mockCocktailApi.Object);
        }

        [Fact]
        public async Task GetBeverages_TestBeverageController_ValidResult_WithEntries()
        {
            string search = "Margarita";
            _mockDbContext.Setup<DbSet<Beverage>>(a => a.Beverages)
                  .ReturnsDbSet(TestDataHelper.GetFakeBeverageListFromDatabase());

            _mockCocktailApi.Setup(s => s.GetBeveragesByName(search))
                 .Returns(TestDataHelper.GetFakeListBeveragesFromApi());


            var result = await _controller.GetBeveragesByName(search);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var beverages = Assert.IsAssignableFrom<List<Beverage>>(okResult.Value);

            Assert.Equal(3, beverages.Count);
        }


        [Fact]
        public async Task GetBeverages_TestBeverageController_ValidResult_WithNoEntriesFromCocktailDbApi()
        {
            string search = "Margarita";
            _mockDbContext.Setup<DbSet<Beverage>>(a => a.Beverages)
                  .ReturnsDbSet(TestDataHelper.GetFakeBeverageListFromDatabase());

            _mockCocktailApi.Setup(s => s.GetBeveragesByName(search))
                 .Returns(TestDataHelper.GetFakeListWithNoBeveragesFromApi());

            var result = await _controller.GetBeveragesByName(search);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var beverages = Assert.IsAssignableFrom<List<Beverage>>(okResult.Value);

            Assert.Single(beverages);
        }

        [Fact]
        public async Task GetBeverages_TestBeverageController_ValidResult_WithNoEntriesFromLocalDatabase()
        {
            string search = "Mojito";
            _mockDbContext.Setup<DbSet<Beverage>>(a => a.Beverages)
                  .ReturnsDbSet(TestDataHelper.GetFakeBeverageListFromDatabase());

            _mockCocktailApi.Setup(s => s.GetBeveragesByName(search))
                 .Returns(TestDataHelper.GetFakeListBeveragesFromApi());

            var result = await _controller.GetBeveragesByName(search);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var beverages = Assert.IsAssignableFrom<List<Beverage>>(okResult.Value);

            Assert.Equal(2, beverages.Count);
        }

        [Fact]
        public async Task GetBeverages_TestBeverageController_ValidResult_WithNoEntries()
        {
            string search = "Mojito";
            _mockDbContext.Setup<DbSet<Beverage>>(a => a.Beverages)
                  .ReturnsDbSet(TestDataHelper.GetFakeBeverageListFromDatabase());

            _mockCocktailApi.Setup(s => s.GetBeveragesByName(search))
                 .Returns(TestDataHelper.GetFakeListWithNoBeveragesFromApi());

            var result = await _controller.GetBeveragesByName(search);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var beverages = Assert.IsAssignableFrom<List<Beverage>>(okResult.Value);

            Assert.Empty(beverages);
        }
    }
}
