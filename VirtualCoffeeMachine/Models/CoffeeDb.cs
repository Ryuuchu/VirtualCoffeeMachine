using Microsoft.EntityFrameworkCore;

namespace VirtualCoffeeMachineContAPI.Models
{
    public class CoffeeDb : DbContext
    {
        public CoffeeDb(DbContextOptions<CoffeeDb> options) : base(options) { }

        public DbSet<Coffee> CoffeeQueue { get; set; } 
    }
}
