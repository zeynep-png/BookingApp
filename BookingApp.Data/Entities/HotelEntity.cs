using BookingApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookingApp.Data.Entities
{
    // Entity class representing a hotel
    public class HotelEntity : BaseEntity
    {
        // The name of the hotel
        public string Name { get; set; }

        // Optional star rating for the hotel (1-5)
        public int? Stars { get; set; }

        // The location of the hotel (e.g., city, address)
        public string Location { get; set; }

        // Type of accommodation (e.g., Hotel, Hostel)
        public AccommodationType AccommodationType { get; set; }

        // Navigation property representing the relationship between hotels and their features
        public ICollection<HotelFeatureEntity> HotelFeatures { get; set; }

        // Navigation property for the rooms associated with the hotel
        public ICollection<RoomEntity> Rooms { get; set; }
    }

    // Configuration class for HotelEntity using fluent API
    public class HotelConfiguration : BaseConfiguration<HotelEntity>
    {
        // Overrides the Configure method to define specific configurations for HotelEntity
        public override void Configure(EntityTypeBuilder<HotelEntity> builder)
        {
            // Configuring the optional star rating property
            builder.Property(x => x.Stars)
                .IsRequired(false); // Indicates that this property is not required

            // Configuring the name property with required validation and a maximum length
            builder.Property(x => x.Name)
                .IsRequired() // Indicates that this property is required
                .HasMaxLength(80); // Sets a maximum length for the name

            // Calls the base configuration to apply common configurations
            base.Configure(builder);
        }
    }
}
