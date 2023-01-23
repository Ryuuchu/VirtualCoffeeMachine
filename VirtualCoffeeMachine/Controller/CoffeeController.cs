using Microsoft.AspNetCore.Mvc;
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
            var coffee = new Coffee
            {
                prepared = DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
                //For April Fools Testcase
                //prepared = new DateTime(2023, 04, 01).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
                message = "Your piping hot coffee is ready"
            };

            _coffee.CoffeeQueue.Add(coffee);
            await _coffee.SaveChangesAsync();

            if(_coffee.CoffeeQueue.Count()%5 == 0)
            {
                return StatusCode(503,"");
            }

            if (coffee.prepared.Contains("04-01"))
            {
                return StatusCode(418,"");
            }

            return Ok(CoffeeToDTO(coffee));
        }

        private static CoffeeDTO CoffeeToDTO(Coffee coffee) =>
            new CoffeeDTO
            {
                message = coffee.message,
                prepared = coffee.prepared,
            };


    }
}

