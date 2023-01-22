using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VirtualCoffeeMachineContAPI.Models;

namespace VirtualCoffeeMachineContAPI.Controller
{
    [Route("/brew-coffee")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly CoffeeDb? _coffee;

        public CoffeeController(CoffeeDb coffee)
        {
            _coffee = coffee;
        }

        [HttpGet("/brew-coffee")]
        public async Task<ActionResult<Coffee>> BrewCoffee()
        {
            int coffeeQueue = _coffee.CoffeeQueue.Count() + 1;
            
            if (coffeeQueue%5 == 0)
            {
                return StatusCode(503);
            }

            _coffee.CoffeeQueue.Add(new Coffee { Id = coffeeQueue+1});
            await _coffee.SaveChangesAsync();

            /*if (_coffee.CoffeeQueue.Contains("04-01"))
            {
                return StatusCode(418);
            }*/

            return await _coffee.CoffeeQueue.Select().Where(c => c;

        }

        private static CoffeeDTO CoffeeToDTO(Coffee coffee) =>
            new CoffeeDTO
            {
                message = coffee.message,
                prepared = coffee.prepared,
            };
    }
}
