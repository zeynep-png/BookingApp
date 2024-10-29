using BookingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Hotel.Dtos
{
    // Data Transfer Object for adding a new hotel in the BookingApp
    public class AddHotelDto
    {
        // Property representing the name of the hotel
        public string Name { get; set; }

        // Nullable property representing the star rating of the hotel
        public int? Stars { get; set; }

        // Property representing the location of the hotel
        public string Location { get; set; }

        // Property representing the type of accommodation (e.g., hotel, hostel)
        public AccommodationType AccommodationType { get; set; }

        // List of feature IDs associated with the hotel
        public List<int> FeatureIds { get; set; }
    }
}
