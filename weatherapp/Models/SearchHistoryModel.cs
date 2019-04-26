
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace weatherapp.Models
{
    public class SearchHistoryModel
    {
        public City Cities { get; set; }
        public class City
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int CityId { get; set; }
            public string CityName { get; set; }
        }
    }
}
