using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Register StoreContext with connection string
builder.Services.AddDbContext<Infrastructure.Data.StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    // Optionally, seed the database here if needed
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
    //var logger = app.Services.GetRequiredService<ILogger<Program>>();
    //logger.LogError(ex, "An error occurred while migrating or initializing the database.");
}

app.Run();







