using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.User.Dtos
{
    // Data Transfer Object for user login
    public class LoginUserDto
    {
        // User's email address, used for authentication
        public string Email { get; set; }

        // User's password, used for authentication
        public string Password { get; set; }
    }
}