using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Hotel.Dtos
{
    // Data Transfer Object representing features of a hotel in the BookingApp
    public class HotelFeaturesDto
    {
        // Property representing the unique identifier of the hotel feature
        public int Id { get; set; }

        // Property representing the title or name of the hotel feature
        public string Title { get; set; }
    }
}
