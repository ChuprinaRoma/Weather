using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Model
{
    public class ModelWeather
    {
        public string Date { get; set; }
        public string TemperatureMax { get; set; }
        public string TemperatureMin { get; set; }
        public string WindSpeed { get; set; }
        public string Cloudiness { get; set; }

        public ModelWeather(string date, string temperatureMax, string tmperatureMin, string windSpeed, string cloudiness)
        {
            Date           = date;
            TemperatureMax = temperatureMax;
            TemperatureMin = tmperatureMin;
            WindSpeed      = windSpeed;
            Cloudiness     = cloudiness;
        }
    }
}
