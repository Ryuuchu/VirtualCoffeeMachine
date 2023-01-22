var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
CoffeeQueue _coffeeQueue = new();

app.MapGet("/brew-coffee", () =>
{
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