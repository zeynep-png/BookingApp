using BookingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.User.Dtos
{
    // Data Transfer Object for user information
    public class UserInfoDto
    {
        // Unique identifier for the user
        public int Id { get; set; }

        // User's email address
        public string Email { get; set; }

        // User's first name
        public string FirstName { get; set; }

        // User's last name
        public string LastName { get; set; }

        // Type of user (e.g., Admin, Regular User) represented by an enumeration
        public UserType UserType { get; set; }
    }
}