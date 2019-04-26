using System;
using System.Threading.Tasks;
using weatherapp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using weatherapp.Repository;
using System.Collections.Generic;

namespace weatherapp.Services
{

    public class WeatherService : IWeatherService
    {
        private readonly IGetWeather _getWeather;

        public WeatherService(IGetWeather getWeather)
        {
            _getWeather = getWeather;
        }

        public async Task<WeatherModel> GetWeatherForecast(string cityName)
        {
            return await _getWeather.ReturnWeatherForecast(cityName);
        }

    }

    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherForecast(string cityName);
    }

    public interface IGetWeather
    {
        Task<WeatherModel> ReturnWeatherForecast(string cityName);
    }
    
    public class GetWeather : IGetWeather
    {
        private readonly HttpClient _httpClient;

        public GetWeather(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<WeatherModel> ReturnWeatherForecast(string cityName)
        {
            const string appid = "fcadd28326c90c3262054e0e6ca599cd";

            var url = new Uri($"http://api.openweathermap.org/data/2.5/forecast?q={cityName},de&appid={appid}&units=imperial");
            var response = await _httpClient.GetAsync(url);
            string json;
            using (var content = response.Content)
            {
                json = await content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<WeatherModel>(json);

        }
    }
}
