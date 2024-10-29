using BookingApp.Business.Types;
using BookingApp.Data.Entities;
using BookingApp.Data.Enums;
using BookingApp.Data.Repositories;
using BookingApp.Data.UnifOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Hotel.Dtos
{
    // The HotelManager class is responsible for managing hotel-related operations
    public class HotelManager : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork; // Unit of Work to manage transactions
        private readonly IRepository<HotelEntity> _hotelRepository; // Repository for hotel entities
        private readonly IRepository<HotelFeatureEntity> _hotelFeatureRepository; // Repository for hotel feature entities

        // Constructor to inject the required dependencies
        public HotelManager(IUnitOfWork unitOfWork, IRepository<HotelEntity> hotelRepository, IRepository<HotelFeatureEntity> hotelFeatureRepository)
        {
            _unitOfWork = unitOfWork;
            _hotelRepository = hotelRepository;
            _hotelFeatureRepository = hotelFeatureRepository;
        }

        // Method to add a new hotel
        public async Task<ServiceMessage> AddHotel(AddHotelDto hotel)
        {
            // Check if a hotel with the same name already exists
            var hasHotel = _hotelRepository.GetAll(x => x.Name.ToLower() == hotel.Name.ToLower()).Any();
            if (hasHotel)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "The Hotel is already exist."
                };
            }

            await _unitOfWork.BeginTransaction(); // Begin a new transaction

            // Create a new hotel entity based on the DTO
            var hotelEntity = new HotelEntity
            {
                Name = hotel.Name,
                Stars = hotel.Stars,
                Location = hotel.Location,
                AccommodationType = hotel.AccommodationType
            };

            // Add the new hotel entity to the repository
            _hotelRepository.Add(hotelEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes to the database
            }
            catch (Exception)
            {
                throw new Exception("Error due adding hotel");
            }

            // Loop through each feature ID to create hotel-feature relationships
            foreach (var featureId in hotel.FeatureIds)
            {
                var hotelFeature = new HotelFeatureEntity
                {
                    HotelId = hotelEntity.Id,
                    FeatureId = featureId
                };

                // Add each feature to the hotel feature repository
                _hotelFeatureRepository.Add(hotelFeature);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes for hotel features
                await _unitOfWork.CommitTransaction(); // Commit the transaction
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction(); // Roll back if an error occurs
                throw new Exception("Error due adding hotel features. Rolling back.");
            }

            return new ServiceMessage
            {
                IsSucceed = true, // Indicate success
            };
        }

        // Method to retrieve a hotel by its ID
        public async Task<HotelDto> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetAll(x => x.Id == id)
                .Select(x => new HotelDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Stars = x.Stars,
                    AccommodationType = x.AccommodationType,
                    Location = x.Location,
                    // Retrieve the features associated with the hotel
                    Features = x.HotelFeatures.Select(f => new HotelFeaturesDto
                    {
                        Id = f.Id,
                        Title = f.Feature.Title
                    }).ToList()
                }).FirstOrDefaultAsync();

            return hotel; // Return the hotel DTO
        }

        // Method to retrieve all hotels
        public async Task<List<HotelDto>> GetAllHotels()
        {
            var hotels = await _hotelRepository.GetAll()
                 .Select(x => new HotelDto
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Stars = x.Stars,
                     AccommodationType = x.AccommodationType,
                     Location = x.Location,
                     // Retrieve features for each hotel
                     Features = x.HotelFeatures.Select(f => new HotelFeaturesDto
                     {
                         Id = f.Id,
                         Title = f.Feature.Title
                     }).ToList()
                 }).ToListAsync();

            return hotels; // Return the list of hotel DTOs
        }

        // Method to adjust the star rating of a hotel
        public async Task<ServiceMessage> AdjustHotelStars(int id, int changeTo)
        {
            var hotel = _hotelRepository.GetById(id);
            if (hotel is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "There is no Hotel with matching this id"
                };
            }

            hotel.Stars = changeTo; // Update the star rating

            _hotelRepository.Update(hotel); // Update the hotel in the repository

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes
            }
            catch (Exception)
            {
                throw new Exception("Error due trying stars number");
            }

            return new ServiceMessage
            {
                IsSucceed = true // Indicate success
            };
        }

        // Method to delete a hotel by its ID
        public async Task<ServiceMessage> DeleteHotel(int id)
        {
            var hotel = _hotelRepository.GetById(id);
            if (hotel is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "The hotel you want to delete is not exist"
                };
            }
            _hotelRepository.Delete(id); // Delete the hotel

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes
            }
            catch (Exception)
            {
                throw new Exception("Error due deleting the hotel");
            }

            return new ServiceMessage
            {
                IsSucceed = true // Indicate success
            };
        }

        // Method to update hotel details
        public async Task<ServiceMessage> UpdateHotel(UpdateHotelDto hotel)
        {
            var hotelEntity = _hotelRepository.GetById(hotel.Id);

            if (hotelEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "The hotel cannot found"
                };
            }

            await _unitOfWork.BeginTransaction(); // Begin a new transaction

            // Update hotel entity properties with new values
            hotelEntity.Name = hotel.Name;
            hotelEntity.Stars = hotel.Stars;
            hotelEntity.AccommodationType = hotel.AccommodationType;
            hotelEntity.Location = hotel.Location;

            _hotelRepository.Update(hotelEntity); // Update the hotel in the repository

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction(); // Roll back if an error occurs
                throw new Exception("Error due updating the hotel");
            }

            // Get the existing features associated with the hotel
            var hotelFeatures = _hotelFeatureRepository
                .GetAll(x => x.HotelId == hotel.Id) // Filter hotel features
                .ToList();

            // Delete existing features
            foreach (var hotelFeature in hotelFeatures)
            {
                _hotelFeatureRepository.Delete(hotelFeature, false);
            }

            // Add new features from the DTO
            foreach (var featureId in hotel.FeatureIds)
            {
                var hotelFeature = new HotelFeatureEntity
                {
                    HotelId = hotelEntity.Id,
                    FeatureId = featureId
                };
                _hotelFeatureRepository.Add(hotelFeature); // Add the feature relationship
            }

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes
                await _unitOfWork.CommitTransaction(); // Commit the transaction
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction(); // Roll back if an error occurs
                throw new Exception("Error due updating the hotel");
            }

            return new ServiceMessage
            {
                IsSucceed = true // Indicate success
            };
        }
    }
}
