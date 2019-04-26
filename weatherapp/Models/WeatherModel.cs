using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace weatherapp.Models
{
    public class WeatherModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WeatherModelId { get; set; }
        [JsonProperty("list")]
        public IEnumerable<Reading> Readings { get; set; }
        public City Cities { get; set; }

    }
    public class Reading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReadingId { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }
        public long Dt { get; set; }
        public DateTime NewDate
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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MainId { get; set; }

        public float Temp { get; set; }
        public int Humidity { get; set; }

    }
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}