using Microsoft.EntityFrameworkCore;

namespace VirtualCoffeeMachine
{
    public class CoffeeMachineDb : DbContext
    {
        public CoffeeMachineDb(DbContextOptions<CoffeeMachineDb> options) : base(options) { }

        public DbSet<CoffeeMachine> CoffeeMachines { get; set; }

        public int Count 
    }
}
