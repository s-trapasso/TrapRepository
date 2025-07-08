using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDesk.Data.Models.Weather
{
    public class WeatherInfo
    {
        public double TemperatureC { get; set; }
        public double WindSpeedKmh { get; set; }
        public string Condition { get; set; } = string.Empty;
    }

}
