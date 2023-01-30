using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System;
using VirtualCoffeeMachineContAPI.Models;

namespace VirtualCoffeeMachineContAPI.Controller
{
    [Route("/brew-coffee")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly CoffeeDb? _coffee;

        //URL for weatherAPI
        string URL = "http://dataservice.accuweather.com/forecasts/v1/daily/1day/22889?";
        string apiKey = "apikey=yyVx9kMJlA0CJQDYGY8D79trkLNKRnBl";

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

            //API integration for weatherAPI
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(URL + apiKey);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonNode weatherForecast = JsonNode.Parse(responseBody)!;

            float temperatureNode = (float)weatherForecast!["DailyForecasts"]![0]!["Temperature"]!["Maximum"]!["Value"];
            //float temperatureNode = 86f;

            if ((temperatureNode - 32) * 0.5556 > 30)
            {
                coffee.message = "Your refreshing iced coffee is ready";
            }

            _coffee.CoffeeQueue.Add(coffee);
            await _coffee.SaveChangesAsync();

            if (_coffee.CoffeeQueue.Count()%5 == 0)
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

