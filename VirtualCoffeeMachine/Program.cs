using Microsoft.EntityFrameworkCore;
using VirtualCoffeeMachineContAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CoffeeDb>(opt => opt.UseInMemoryDatabase("CoffeeQueue"));
builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();

app.Run();