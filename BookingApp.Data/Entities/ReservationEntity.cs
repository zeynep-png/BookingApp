using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Entities
{
    // Entity class representing a reservation in the booking application
    public class ReservationEntity : BaseEntity
    {
        // Foreign key referencing the room being reserved
        public int RoomId { get; set; }

        // Foreign key referencing the user making the reservation
        public int UserId { get; set; }

        // The start date of the reservation
        public DateTime StartDate { get; set; }

        // The end date of the reservation
        public DateTime EndDate { get; set; }

        // The number of guests for the reservation
        public int GuestCount { get; set; }

        // Navigation property representing the related user
        public UserEntity User { get; set; }

        // Navigation property representing the related room
        public RoomEntity Room { get; set; }
    }

    // Configuration class for ReservationEntity using fluent API
    public class ReservationConfiguration : BaseConfiguration<ReservationEntity>
    {
        // Overrides the Configure method to define specific configurations for ReservationEntity
        public override void Configure(EntityTypeBuilder<ReservationEntity> builder)
        {
            // Calls the base configuration to apply common configurations
            base.Configure(builder);
        }
    }
}
