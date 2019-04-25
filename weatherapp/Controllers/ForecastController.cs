using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weatherapp.Services;
using weatherapp.Models;
using weatherapp.Repository;

namespace weatherapp.Controllers
{

    [Route("api/forecast/{cityName}")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        private readonly ISearchHistoryRepository<WeatherModel> _searchHistoryRepository;

        public ForecastController(IWeatherService weatherService, ISearchHistoryRepository<WeatherModel> searchHistoryRepository)
        {
            _weatherService = weatherService;
            _searchHistoryRepository = searchHistoryRepository;
      
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string cityName)
        {
            WeatherModel result = await _weatherService.GetWeatherForecast(cityName);
            _searchHistoryRepository.Add(result);
            return Ok(result);
        }

    }
}
