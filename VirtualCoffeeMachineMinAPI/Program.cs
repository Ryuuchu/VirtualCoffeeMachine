using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
CoffeeQueue _coffeeQueue = new();

string URL = "http://dataservice.accuweather.com/forecasts/v1/daily/1day/22889?";
string apiKey = "apikey=yyVx9kMJlA0CJQDYGY8D79trkLNKRnBl";

app.MapGet("/brew-coffee", async () =>
{
    HttpClient client = new HttpClient();
    HttpResponseMessage response = await client.GetAsync(URL+apiKey);
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();

    JsonNode weatherForecast = JsonNode.Parse(responseBody)!;

    float temperatureNode = (float)weatherForecast!["DailyForecasts"]![0]!["Temperature"]!["Maximum"]!["Value"];
    //Test case for over 30°C
    //float temperatureNode = 88f;

    if (_coffeeQueue.Count() == 4)
    {
        _coffeeQueue.Clear();
        return Results.StatusCode(503);
    }

    Coffee coffee = new Coffee();
    _coffeeQueue.AddCoffee(coffee);

    if (coffee.prepared.Contains("04-01"))
    {
        return Results.StatusCode(418);
    }

    if((temperatureNode - 32)*0.5556 > 30)
    {
        coffee.message = "Your refreshing iced coffee is ready";
    }

    return Results.Ok(coffee);
});

app.Run();

class Coffee
{
    private static string COFFEE = "Your piping hot coffee is ready", TEA = "I’m a teapot";
    public string message { get; set; }
    public string prepared { get; set; }
    public Coffee()
    {
        prepared = DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz");
        if (prepared.Contains("04-01"))
        {
            message = TEA;
        }
        else
            message = COFFEE;
    }
}

class CoffeeQueue
{
    private readonly List<Coffee> coffeeQueue = new();

    public void AddCoffee(Coffee coffee) { coffeeQueue.Add(coffee); }

    public int Count() { return coffeeQueue.Count; }

    public void Clear() { coffeeQueue.Clear(); }
}