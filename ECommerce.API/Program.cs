using ECommerce.API;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Persistence.Seeding;
using ECommerce.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddPresentation(); 
builder.Services.AddInfrastructure(builder.Configuration); 
builder.Services.AddApplication(); 

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
var dbSeed = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
await dbSeed.SeedAll();
// Configure the HTTP request pipeline.

app.Run();