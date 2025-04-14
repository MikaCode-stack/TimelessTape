using Microsoft.EntityFrameworkCore;
using TimelessTapes.Data;
using TimelessTapes.Services;

var builder = WebApplication.CreateBuilder(args);

// Adding DbContext with SQL Server connection
builder.Services.AddDbContext<DBHandler>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TimelessTapeDb")));

//Adding controllers and session storage
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

//Adding services
builder.Services.AddScoped<TransactionService>();


// Adding Swagger for development
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
app.UseSession();  // Enable session state
app.UseAuthorization();     // Use Authorization middleware

app.MapControllers();  // Map controllers to endpoints


app.Run(); // Run the application

