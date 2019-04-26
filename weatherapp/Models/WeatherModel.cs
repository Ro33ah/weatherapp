using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace weatherapp.Models
{
    public class WeatherModel
    {
        [JsonProperty("list")]
        public IEnumerable<Reading> Readings { get; set; }
   

    }
    public class Reading
    {
        
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("dt")]
        public long DayTime { get; set; }

        public DateTime UtcTime
        {
            get
            {
                //Dt /= 1000;
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddSeconds(DayTime);
                return date;
                // return NewDate.Date.ToString("d");
            }
        }
    }
    public class Main
    {
        [JsonProperty("temp")]
        public float Temp { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }

    }
    
}