using BookingApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookingApp.WebApi.Models
{
    public class UpdateHotelRequest
    {
        [Required]
        public string Name { get; set; }
        public int? Stars { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public AccommodationType AccommodationType { get; set; }
        public List<int> FeatureIds { get; set; }
    }
}
