using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace weatherapp.Models
{
    public class WeatherModel
    {
        public IEnumerable<Reading> List { get; set; }
        public City city { get; set; }

    }
    public class Reading
    {
        [JsonProperty("main")]
        public Main Main { get; set; }
        public long Dt { get; set; }
        public DateTime newDate
        {
            get
            {
                //Dt /= 1000;
                DateTime newDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                newDate = newDate.AddSeconds(Dt);
                return newDate;
                // return NewDate.Date.ToString("d");
            }
        }
    }
    public class Main
    {
        public float temp { get; set; }
        public int humidity { get; set; }

    }
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}

