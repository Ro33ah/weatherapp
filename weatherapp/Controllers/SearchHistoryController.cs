
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using weatherapp.Models;
using weatherapp.Repository;


namespace weatherapp.Controllers
{
    [Route("api/history")]
    public class SearchHistoryController : Controller
    {
        private readonly ISearchHistoryRepository<SearchHistoryModel> _searchHistory;
        public SearchHistoryController(ISearchHistoryRepository<SearchHistoryModel> searchHistory)
        {
            _searchHistory = searchHistory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<SearchHistoryModel> searchHistoryModels = _searchHistory.GetSearchHistoryAll();
            return Ok(searchHistoryModels);
        }
    }
}
