using BookingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Hotel.Dtos
{
    // UpdateHotelDto is used for updating existing hotel details
    public class UpdateHotelDto
    {
        // Unique identifier for the hotel that needs to be updated
        public int Id { get; set; }

        // Name of the hotel; this field is required
        [Required]
        public string Name { get; set; }

        // Star rating of the hotel (optional)
        public int? Stars { get; set; }

        // Location of the hotel; this field is required
        [Required]
        public string Location { get; set; }

        // Type of accommodation; this field is required
        [Required]
        public AccommodationType AccommodationType { get; set; }

        // List of feature IDs associated with the hotel (optional)
        public List<int> FeatureIds { get; set; }
    }
}
