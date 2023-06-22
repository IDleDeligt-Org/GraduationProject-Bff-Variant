using GraduationProject;
using GraduationProject.CocktailDB;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

//var connection = String.Empty;                                                            <<<<NEEDED FOR AZURE
//builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");          <<<<NEEDED FOR AZURE
//connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");     <<<<NEEDED FOR AZURE

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddDbContext<IApplicationDbContext, GraduationProject.ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.UseSqlServer(connection);                                                     <<<<NEEDED FOR AZURE
});

builder.Services.AddHttpClient<ICocktailDBApi, CocktailDBApi>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://www.thecocktaildb.com");
});


// Register the Swagger generator
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Oidc:Authority"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


// Add the Swagger middleware for serving the generated JSON document and Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    // Serve the Swagger UI at the app's root (http://localhost:<port>/)
    c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.Run();
