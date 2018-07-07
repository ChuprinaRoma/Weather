using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Weather.Model;

namespace Weather
{
    public class ConnectorWeathermap
    {
        private  WebRequest request = null;
        private string key          = "ab1c1f624d89c363d3b1035bfbd757ae";


        //Настройка запроса на API погоды
        private  void Connect(string typeWeather, string nameCyti)
        {
            request        = WebRequest.Create($"https://api.openweathermap.org/data/2.5/{typeWeather}?q={nameCyti}&units=Metric&appid={key}");
            request.Method = "GET"; 
        }


        public  string GetWeather(string NameCity, string typeWeather)
        {
            string responseStr = null;
            try
            {
                Connect(typeWeather, NameCity);
                responseStr = ReqRequestves();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return responseStr;
        }


        //Метод отправляет запрос на API погоды и возвращает ответ (JSON строку)
        private  string ReqRequestves()
        {
            string responseStr = null;
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseStr = reader.ReadToEnd();
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return responseStr;
        }
    }
}
