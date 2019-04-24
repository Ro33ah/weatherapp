
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using weatherapp.Models;
using weatherapp.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace weatherapp.Controllers
{
    [Route("api/weathermodels")]
    public class HistoryController : Controller
    {
        private IHistoryRepository<WeatherModel> _historyRepository;
        private HistoryController(IHistoryRepository<WeatherModel> historyRepository)
        {
            _historyRepository = historyRepository;
        }
        // GET: api/weathermodels
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<WeatherModel> weatherModels = _historyRepository.GetHistoryAll();
            return Ok(weatherModels);
        }
  
        // POST api/weathermodels
        [HttpPost]
        public void Post([FromBody] WeatherModel weatherModel)
        {
            _historyRepository.Add(weatherModel);
            
        }
    }
}
