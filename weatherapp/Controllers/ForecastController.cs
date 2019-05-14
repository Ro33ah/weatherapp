using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weatherapp.Services;
using weatherapp.Models;
using weatherapp.Repository;
using Microsoft.Extensions.Logging;

namespace weatherapp.Controllers
{

    [Route("api/forecast/{cityName}")]
    [ApiController]

    public class ForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        private readonly ISearchHistoryRepository<SearchHistoryModel> _searchHistoryRepository;

        private readonly ILogger logger;

        public ForecastController(IWeatherService weatherService, 
                ISearchHistoryRepository<SearchHistoryModel> searchHistoryRepository, 
                ILogger<ForecastController> _logger)
        {
            _weatherService = weatherService;
            _searchHistoryRepository = searchHistoryRepository;
            logger = _logger;
      
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string cityName)
        {
            logger.Log(LogLevel.Information, $"Fetching weather info for {cityName}");

            WeatherModel result = await _weatherService.GetWeatherForecast(cityName);
            if (result.Readings == null)
            {
                return NotFound();
            }
            else
            {
                _searchHistoryRepository.UpdateHistory(new SearchHistoryModel
                {
                    CityName = cityName
                });

                logger.Log(LogLevel.Information, $"weather info for {cityName}: response OK");

                return Ok(result);
            }
        }

    }
}
