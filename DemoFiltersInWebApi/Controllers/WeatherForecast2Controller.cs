using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoFiltersInWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
  
    public class WeatherForecast2Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecast2Controller> _logger;

        public WeatherForecast2Controller(ILogger<WeatherForecast2Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
      
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
       
        [HttpGet("data")]
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
