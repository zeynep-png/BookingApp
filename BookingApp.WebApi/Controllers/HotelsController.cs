using BookingApp.Business.Operations.Hotel.Dtos;
using BookingApp.WebApi.Filters;
using BookingApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService; // Hotel service for handling hotel operations

        // Constructor accepting the hotel service via dependency injection
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService; // Initialize the hotel service
        }

        // Endpoint for retrieving a specific hotel by ID
        [HttpGet("{id}")] // Corrected the route for hotel ID retrieval
        public async Task<IActionResult> GetHotel(int id)
        {
            var hotel = await _hotelService.GetHotel(id); // Get hotel details by ID

            // Check if the hotel exists
            if (hotel is null)
                return NotFound(); // Return 404 if not found
            else
                return Ok(hotel); // Return hotel details if found
        }

        // Endpoint for retrieving all hotels
        [HttpGet] // This method will respond to GET requests without an ID
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelService.GetAllHotels(); // Get a list of all hotels

            return Ok(hotels); // Return the list of hotels
        }

        // Endpoint for adding a new hotel
        [HttpPost] // This method will respond to POST requests
        [Authorize(Roles = "Admin")] // Only users with the Admin role can access this endpoint
        public async Task<IActionResult> AddHotel(AddHotelRequest request)
        {
            // Create DTO for adding a hotel
            var addHotelDto = new AddHotelDto
            {
                Name = request.Name, // Set the hotel name from the request
                Stars = request.Stars, // Set the star rating from the request
                Location = request.Location, // Set the hotel location
                AccommodationType = request.AccommodationType, // Set the accommodation type
                FeatureIds = request.FeatureIds // Set feature IDs for the hotel
            };

            // Attempt to add the hotel using the hotel service
            var result = await _hotelService.AddHotel(addHotelDto);

            // Check if hotel addition succeeded
            if (!result.IsSucceed)
            {
                return BadRequest(result.Message); // Return error message if not successful
            }
            else
            {
                return Ok(); // Return OK response if successful
            }
        }

        // Endpoint for adjusting the star rating of a hotel
        [HttpPatch("{id}/stars")] // This method will respond to PATCH requests for star adjustments
        [Authorize(Roles = "Admin")] // Only users with the Admin role can access this endpoint
        public async Task<IActionResult> AdjustHotelStars(int id, int changeTo)
        {
            var result = await _hotelService.AdjustHotelStars(id, changeTo); // Adjust the stars of the specified hotel

            // Check if the operation succeeded
            if (!result.IsSucceed)
                return NotFound(result.Message); // Return 404 if not found
            else
                return Ok(); // Return OK response if successful
        }

        // Endpoint for deleting a hotel by ID
        [HttpDelete("{id}")] // This method will respond to DELETE requests
        [Authorize(Roles = "Admin")] // Only users with the Admin role can access this endpoint
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _hotelService.DeleteHotel(id); // Attempt to delete the hotel

            // Check if the operation succeeded
            if (!result.IsSucceed)
                return NotFound(result.Message); // Return 404 if not found
            else
                return Ok(); // Return OK response if successful
        }

        // Endpoint for updating hotel details
        [HttpPut("{id}")] // This method will respond to PUT requests for updates
        [Authorize(Roles = "Admin")] // Only users with the Admin role can access this endpoint
        [TimeControlFilter] // Apply a custom time control filter for this action
        public async Task<IActionResult> UpdateHotel(int id, UpdateHotelRequest request)
        {
            // Create DTO for updating hotel details
            var updateHotelDto = new UpdateHotelDto
            {
                Id = id, // Set the hotel ID
                Name = request.Name, // Set the updated hotel name
                Stars = request.Stars, // Set the updated star rating
                Location = request.Location, // Set the updated location
                AccommodationType = request.AccommodationType, // Set the updated accommodation type
                FeatureIds = request.FeatureIds // Set the updated feature IDs
            };

            var result = await _hotelService.UpdateHotel(updateHotelDto); // Attempt to update the hotel

            // Check if the update succeeded
            if (!result.IsSucceed)
                return NotFound(result.Message); // Return 404 if not found
            else
                return await GetHotel(id); // Return the updated hotel details
        }
    }
}
