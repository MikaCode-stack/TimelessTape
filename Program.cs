using Microsoft.EntityFrameworkCore;
using TimelessTapes.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server connection
builder.Services.AddDbContext<DBHandler>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TimelessTapeDb")));
builder.Services.AddControllers();

// Add Swagger for development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Enable Swagger UI for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  // Enforce HTTPS
app.UseAuthorization();     // Use Authorization middleware (if applicable)

app.MapControllers();  // Map controllers to endpoints

app.Run();