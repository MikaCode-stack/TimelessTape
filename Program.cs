using Microsoft.EntityFrameworkCore;
using TimelessTapes.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBHandler>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TimelessTapeDb")));
builder.Services.AddControllers();
    
WebApplication app = builder.Build();

app.MapControllers();
app.Run();

