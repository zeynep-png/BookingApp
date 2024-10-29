using BookingApp.Business.DataProtection;
using BookingApp.Business.Operations.User.Dtos;
using BookingApp.Business.Types;
using BookingApp.Data.Entities;
using BookingApp.Data.Enums;
using BookingApp.Data.Repositories;
using BookingApp.Data.UnifOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.User
{
    // Implementation of user-related operations defined in IUserService
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork; // Unit of work for managing database transactions
        private readonly IRepository<UserEntity> _userRepository; // Repository for user entity access
        private readonly IDataProtection _protector; // Service for protecting user data like passwords

        // Constructor to initialize dependencies via Dependency Injection
        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _protector = protector;
        }

        // Asynchronous method to add a new user to the system
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            // Check if the email already exists in the repository (case insensitive)
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());

            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Email is already exist" // Message if the email is already registered
                };
            }

            // Create a new user entity with protected password and default user type
            var userEntity = new UserEntity()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = _protector.Protect(user.Password), // Protect the password before storing
                BirthDate = user.BirthDate,
                UserType = UserType.Customer // Default user type
            };

            _userRepository.Add(userEntity); // Add the new user entity to the repository

            try
            {
                await _unitOfWork.SaveChangesAsync(); // Save changes to the database asynchronously
            }
            catch (Exception)
            {
                throw new Exception("ERROR"); // Handle exceptions during saving
            }

            // Return success message if user is added successfully
            return new ServiceMessage
            {
                IsSucceed = true
            };
        }

        // Method to log in a user and return user information
        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            // Retrieve the user entity by email (case insensitive)
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());

            if (userEntity is null) // Check if the user exists
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Wrong Email or password." // Message if email is incorrect
                };
            }

            // Unprotect the stored password to compare with the provided password
            var unprotectedPassword = _protector.UnProtect(userEntity.Password);
            if (unprotectedPassword == user.Password) // Check if the passwords match
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        UserType = userEntity.UserType // Return user information if login is successful
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Wrong password." // Message if the password is incorrect
                };
            }
        }
    }
}
