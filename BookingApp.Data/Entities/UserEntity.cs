using BookingApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Entities
{
    // Entity class representing a user in the booking system
    public class UserEntity : BaseEntity
    {
        // User's email address
        public string Email { get; set; }

        // User's password (should be stored securely)
        public string Password { get; set; }

        // User's first name
        public string FirstName { get; set; }

        // User's last name
        public string LastName { get; set; }

        // User's date of birth
        public DateTime BirthDate { get; set; }

        // Type of user (e.g., Customer, Admin)
        public UserType UserType { get; set; }

        // Relational property: List of reservations made by the user
        public ICollection<ReservationEntity> Reservations { get; set; }
    }

    // Configuration class for UserEntity
    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            // Configuring the FirstName property
            builder.Property(x => x.FirstName)
                .IsRequired() // FirstName is a required field
                .HasMaxLength(50); // Maximum length of FirstName is 50 characters

            // Configuring the LastName property
            builder.Property(x => x.LastName)
                .IsRequired() // LastName is a required field
                .HasMaxLength(50); // Maximum length of LastName is 50 characters

            // Call the base configuration
            base.Configure(builder);
        }
    }
}
