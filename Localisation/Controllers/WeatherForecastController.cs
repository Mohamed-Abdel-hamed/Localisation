using Localisation.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Localisation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private static readonly string[] array = new string[5];

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStringLocalizer<SharedResources> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("UseLocalisation")]
        public IActionResult GetAll()
        {
           var arr=  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = array[Random.Shared.Next(array.Length)]
            })
            .ToArray();
            if(arr.Length <= 10)
                return NotFound(_localizer[SharedResourcesKeys.NotFound]);
            return Ok(arr);
        }
    }
}