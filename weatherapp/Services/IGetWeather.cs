using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weatherapp.Models;

namespace weatherapp.Services
{
    public interface IGetWeather
    {
        Task<WeatherModel> ReturnWeatherForecast(int CityID);
    }
}
