using BookingApp.Business.Operations.User.Dtos;
using BookingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.User
{
    // Interface defining the operations related to user management
    public interface IUserService
    {
        // Asynchronous method to add a new user
        Task<ServiceMessage> AddUser(AddUserDto user);

        // Method to log in a user and return user information along with a service message
        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}