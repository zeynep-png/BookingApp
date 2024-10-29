using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Entities
{
    // Entity class representing a room in a hotel
    public class RoomEntity : BaseEntity
    {
        // Foreign key referencing the hotel to which the room belongs
        public int HotelId { get; set; }

        // The room number for identification
        public string RoomNumber { get; set; }

        // Relational property representing the reservations associated with this room
        public ICollection<ReservationEntity> Reservations { get; set; }

        // Navigation property representing the related hotel
        public HotelEntity Hotel { get; set; }
    }

    // Configuration class for RoomEntity using fluent API
    public class RoomConfiguration : BaseConfiguration<RoomEntity>
    {
        // Overrides the Configure method to define specific configurations for RoomEntity
        public override void Configure(EntityTypeBuilder<RoomEntity> builder)
        {
            // Calls the base configuration to apply common configurations
            base.Configure(builder);
        }
    }
}