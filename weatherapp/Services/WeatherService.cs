using System.Threading.Tasks;
using weatherapp.Models;


namespace weatherapp.Services
{
    public class WeatherService : IWeatherService
    {
        private static IGetWeather _getWeather;
        public WeatherService(IGetWeather getWeather)
        {
            _getWeather = getWeather;
        }
        public async Task<WeatherModel> GetWeatherForecast(int CityID)
        {
            return await _getWeather.ReturnWeatherForecast(CityID);
        }
    }
}
