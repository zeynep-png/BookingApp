using BookingApp.Data.Enums; // Importing enums used in the application
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.User.Dtos
{
    // Data Transfer Object for adding a new user
    public class AddUserDto
    {
        // User's email address
        public string Email { get; set; }

        // User's password
        public string Password { get; set; }

        // User's first name
        public string FirstName { get; set; }

        // User's last name
        public string LastName { get; set; }

        // User's birth date
        public DateTime BirthDate { get; set; }
    }
}
