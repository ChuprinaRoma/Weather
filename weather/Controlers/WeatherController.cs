using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Weather.Model;

namespace Weather.Controlers
{
    public class WeatherController : Controller
    {
        //Контролер для погоды на один день
        [HttpGet("api/CurentWeather/{NameCity}")]
        public IActionResult GetWeatherToDayly(string NameCity)
        {
            IActionResult actionResult              = null;
            ConnectorWeathermap connectorWeathermap = null;
            try
            {
                connectorWeathermap      = new ConnectorWeathermap();
                string fullWearther      = connectorWeathermap.GetWeather(NameCity, "weather");
                JObject json             = JObject.Parse(fullWearther);
                string data = DateTime.Today.ToShortDateString();
                ModelWeather modelWeather = GetlWeather(json, data);
                actionResult              = Ok(modelWeather);
            }
            catch (Exception e)
            {
                actionResult = NotFound(e.Message);
            }
            return actionResult;
        }

        //Контролер для погоды на 5 деней
        [HttpGet("api/GetForecast/{NameCity}")]
        public IActionResult GetGetWeatherOnWeekly(string NameCity)
        {
            IActionResult actionResult              = null;
            ConnectorWeathermap connectorWeathermap = null;
            try
            {
                connectorWeathermap                 = new ConnectorWeathermap();
                string fullWearther                 = connectorWeathermap.GetWeather(NameCity, "forecast");
                JToken[] jToken                     = JObject.Parse(fullWearther)["list"].ToArray();
                List<ModelWeather> listModelWeather = GetWeatherToWeekly(jToken);
                actionResult                        = Ok(listModelWeather);
            }
            catch (Exception e)
            {
                actionResult = NotFound(e.Message);
            }
            return actionResult;
        }

        //Метод возращает список погоды на 5 дней или 6 включая прошедший сегодняшний день
        private List<ModelWeather> GetWeatherToWeekly(JToken[] jToken)
        {
            List<ModelWeather> listModelWeather = new List<ModelWeather>();
            DateTime timetWeather               = DateTime.Now.Date.AddHours(10);
            try
            {
                for (int i = 0; i < jToken.Length; i++)
                {
                    var json      = JObject.Parse(jToken[i].ToString());
                    DateTime date = Convert.ToDateTime(json["dt_txt"].ToString());
                    if (timetWeather.Ticks <= date.Ticks)
                    {
                        ModelWeather modelWeather = GetlWeather(json, date.ToShortDateString());
                        timetWeather              = timetWeather.AddDays(1);
                        listModelWeather.Add(modelWeather);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listModelWeather;
        }

        //Метод распарсивает запрос погоды (JObject(JSON)) и возращает модель ответа
        private ModelWeather GetlWeather(JObject json, string data)
        {
            ModelWeather modelWeather = null;
            try
            {
                string dataWeathre = data;
                string temp_max = json["main"]["temp_max"].ToString();
                string temp_min = json["main"]["temp_min"].ToString();
                string speedWind = json["wind"]["speed"].ToString();
                string weather = json["weather"][0]["description"].ToString();
                modelWeather = new ModelWeather(data, temp_max + " C", temp_min + " C", speedWind + " М/С", weather);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return modelWeather;
        }

        //Примеры городов для погода:BANKRA, LONDON, TELMANKEND, GUSTAVIA, WHYALLA, MEKELE
        /////////////////////////////////////////////////////////////////////////////////
    }
}