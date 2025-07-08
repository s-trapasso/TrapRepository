using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Models.Weather;

namespace CarDesk.Data.Services
{
    public class OpenMeteoService
    {
        private readonly HttpClient _http;

        public OpenMeteoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<WeatherInfo?> GetCurrentWeatherAsync(double lat, double lon)
        {
            var url = $"https://api.open-meteo.com/v1/forecast" 
                + $"?latitude={lat.ToString(CultureInfo.InvariantCulture)}&longitude={lon.ToString(CultureInfo.InvariantCulture)}" 
                +  "&current_weather=true";                      // ✅ flag giusto


            var json = await _http.GetFromJsonAsync<MeteoResponse>(url);    //DA VERIFICARE DESERIALIZZAZIONE JSON DI RISPOSTA
            var current = json?.current_weather;
            if (current == null) return null;

            return new WeatherInfo
            {
                TemperatureC = current.temperature,
                WindSpeedKmh = current.windspeed,
                Condition = WeatherEmoji(current.weathercode)
            };
        }

        private static string WeatherEmoji(int code) => code switch
        {
            0 => "☀️ Clear",
            1 or 2 => "⛅ Partly Cloudy",
            3 => "☁️ Cloudy",
            45 or 48 => "🌫️ Fog",
            51 or 53 or 55 => "🌦️ Drizzle",
            61 or 63 or 65 => "🌧️ Rain",
            71 or 73 or 75 => "❄️ Snow",
            95 => "⛈️ Thunderstorm",
            _ => "❓ Unknown"
        };

        private sealed class MeteoResponse
        {
            public CurrentWeather current_weather { get; set; } = new();
        }

        private sealed class CurrentWeather
        {
            public double temperature { get; set; }
            public double windspeed { get; set; }
            public int weathercode { get; set; }
        }

    }

}
