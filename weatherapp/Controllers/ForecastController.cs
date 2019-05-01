﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weatherapp.Services;
using weatherapp.Models;
using weatherapp.Repository;
using System;

namespace weatherapp.Controllers
{

    [Route("api/forecast/{cityName}")]
    [ApiController]

    public class ForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        private readonly ISearchHistoryRepository<SearchHistoryModel> _searchHistoryRepository;

        public ForecastController(IWeatherService weatherService, ISearchHistoryRepository<SearchHistoryModel> searchHistoryRepository)
        {
            _weatherService = weatherService;
            _searchHistoryRepository = searchHistoryRepository;
      
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(string cityName)
        {
          
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

                return Ok(result);
            }
        }

    }
}
