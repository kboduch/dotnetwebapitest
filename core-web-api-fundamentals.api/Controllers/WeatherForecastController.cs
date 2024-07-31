using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;

namespace core_web_api_fundamentals.api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public PaginatedResult<WeatherForecast> Get()
    {
        var data = Enumerable
            .Range(1, Random.Shared.Next(minValue: 1, maxValue: 15))
            .Select(
                index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(minValue: -20, maxValue: 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }
            )
            .ToArray();

        return new PaginatedResult<WeatherForecast> { Items = data };
    }

    public class PaginatedResult<T>
    {
        //Auto-implemented property
        public int TotalItems { get; private init; }

        //This approach requires additional swagger and Json serialization config - the field didn't show at all.
        // public readonly IReadOnlyList<T> Items;
        // public PaginatedResult(IReadOnlyList<T> items = null)
        // {
        //     Items = items ?? new List<T>();
        //     TotalItems = Items.Count;
        // }
        //
        // public IReadOnlyList<T> GetItems()
        // {
        //     return Items;
        // }


        // Property with backing field
        private readonly IReadOnlyList<T> _items = new List<T>();

        public IReadOnlyList<T> Items
        {
            get => _items;
            init
            {
                _items = value;
                TotalItems = _items.Count;
            }
        }
    }
}