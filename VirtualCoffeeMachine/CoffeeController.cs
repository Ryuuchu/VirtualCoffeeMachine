using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace VirtualCoffeeMachine
{
    [Route("/brew-coffee")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        static string coffee = "Your piping hot coffee is ready";
        static string tea = "I’m a teapot";
        int count;

        [HttpGet("/brew-coffee")]
        public async Task<IActionResult> GetCoffee(int count)
        {
            if(count%5 == 0)
            {
                return StatusCode(503, new CoffeeMachine() { messageStatus = tea, prepared = DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"), count = count });
            }

            count++;

            return Ok(new CoffeeMachine() { messageStatus = coffee, prepared = DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"), count = count });
        }
    }
}
