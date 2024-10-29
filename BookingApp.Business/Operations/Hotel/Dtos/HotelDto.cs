using BookingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Hotel.Dtos
{
    // Data Transfer Object representing a hotel in the BookingApp
    public class HotelDto
    {
        // Property representing the unique identifier of the hotel
        public int Id { get; set; }

        // Property representing the name of the hotel
        public string Name { get; set; }

        // Nullable property representing the star rating of the hotel
        public int? Stars { get; set; }

        // Property representing the location of the hotel
        public string Location { get; set; }

        // Property representing the type of accommodation (e.g., hotel, hostel)
        public AccommodationType AccommodationType { get; set; }

        // List of hotel features associated with this hotel
        public List<HotelFeaturesDto> Features { get; set; }
    }
}
