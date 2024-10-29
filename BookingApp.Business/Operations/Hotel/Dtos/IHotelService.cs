using BookingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Hotel.Dtos
{
    // IHotelService interface defines the contract for hotel-related operations
    public interface IHotelService
    {
        // Method to add a new hotel; returns a ServiceMessage indicating success or failure
        Task<ServiceMessage> AddHotel(AddHotelDto hotel);

        // Method to retrieve a hotel by its ID; returns the hotel details as HotelDto
        Task<HotelDto> GetHotel(int id);

        // Method to retrieve a list of all hotels; returns a list of HotelDto objects
        Task<List<HotelDto>> GetAllHotels();

        // Method to adjust the star rating of a hotel; returns a ServiceMessage indicating success or failure
        Task<ServiceMessage> AdjustHotelStars(int id, int changeTo);

        // Method to delete a hotel by its ID; returns a ServiceMessage indicating success or failure
        Task<ServiceMessage> DeleteHotel(int id);

        // Method to update an existing hotel's details; returns a ServiceMessage indicating success or failure
        Task<ServiceMessage> UpdateHotel(UpdateHotelDto hotel);
    }
}
