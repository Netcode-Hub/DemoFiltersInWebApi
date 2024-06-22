using DemoFiltersInWebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoFiltersInWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public  IActionResult Get()
        {
            if (!CheckAuthState()) return Unauthorized();

            return Ok( Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }
        [HttpGet("data")]
        [ServiceFilter(typeof(CheckAuthFilter))]
        public IActionResult GetData()
        {
            if (!CheckAuthState()) return Unauthorized();
            return Ok("New data");
        }

        private bool CheckAuthState()
        {
            var authState = HttpContext.Request.Headers["AuthenticationState"].FirstOrDefault();
            return Convert.ToBoolean(authState);

        }
    }
}
