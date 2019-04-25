
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using weatherapp.Models;
using weatherapp.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace weatherapp.Controllers
{
    [Route("api/history")]
    public class SearchHistoryController : Controller
    {
        private readonly ISearchHistoryRepository<WeatherModel> _searchHistory;
        private SearchHistoryController(ISearchHistoryRepository<WeatherModel> searchHistory)
        {
            _searchHistory = searchHistory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<WeatherModel> weatherModels = _searchHistory.GetSearchHistoryAll();
            return Ok(weatherModels);
        }
    }
}
