using Microsoft.EntityFrameworkCore;
using TimelessTapes.Data;
using TimelessTapes.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

var builder = WebApplication.CreateBuilder(args);

// Adding DbContext with SQL Server connection
builder.Services.AddDbContext<DBHandler>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TimelessTapeDb")));

// Add other necessary services
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new() { Title = "TimelessTapes API", Version = "v1" });
});

WebApplication app = builder.Build();


app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimelessTapes API v1");
        c.RoutePrefix = "api/docs"; // Custom Swagger path instead of root
    });
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSession(); // Enable session state
app.UseAuthorization(); // Authorization middleware

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();

