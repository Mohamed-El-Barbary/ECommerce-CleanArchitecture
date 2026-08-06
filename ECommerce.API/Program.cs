using ECommerce.API;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Persistence.Data.DbContexts;
using ECommerce.Infrastructure.Persistence.Seeding;
using ECommerce.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var dbSeed = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

    await dbContext.Database.MigrateAsync();
    await dbSeed.SeedAll();
}
// Configure the HTTP request pipeline.

app.Run();