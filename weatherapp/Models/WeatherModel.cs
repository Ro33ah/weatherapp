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
        public int weatherModelId { get; set; }
        public IEnumerable<Reading> List { get; set; }
        public City city { get; set; }

    }
    public class Reading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int readingId { get; set; }

        [JsonProperty("main")]
        public Main main { get; set; }
        public long dt { get; set; }
        public DateTime newDate
        {
            get
            {
                //Dt /= 1000;
                DateTime newDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                newDate = newDate.AddSeconds(dt);
                return newDate;
                // return NewDate.Date.ToString("d");
            }
        }
    }
    public class Main
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int mainId { get; set; }

        public float temp { get; set; }
        public int humidity { get; set; }

    }
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cityId { get; set; }
        public string name { get; set; }
    }
}