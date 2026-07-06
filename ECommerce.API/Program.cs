using ECommerce.API;
using ECommerce.Infrastructure;
using ECommerce.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddPresentation(); 
builder.Services.AddInfrastructure(); 
builder.Services.AddApplication(); 

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();