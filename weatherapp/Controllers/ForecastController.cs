using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weatherapp.Services;

namespace weatherapp.Controllers
{

    [Route("data/2.5/forecast/{cityId}")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public ForecastController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string cityId)
        {
            var result = await _weatherService.GetWeatherForecast(cityId);
            return Ok(result);
        }

    }
}
