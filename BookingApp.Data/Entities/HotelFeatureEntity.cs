using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Entities
{
    // Entity class representing the relationship between hotels and their features
    public class HotelFeatureEntity : BaseEntity
    {
        // Foreign key referencing the hotel
        public int HotelId { get; set; }

        // Foreign key referencing the feature
        public int FeatureId { get; set; }

        // Navigation property representing the related hotel
        public HotelEntity Hotel { get; set; }

        // Navigation property representing the related feature
        public FeatureEntity Feature { get; set; }
    }

    // Configuration class for HotelFeatureEntity using fluent API
    public class HotelFeatureConfiguration : BaseConfiguration<HotelFeatureEntity>
    {
        // Overrides the Configure method to define specific configurations for HotelFeatureEntity
        public override void Configure(EntityTypeBuilder<HotelFeatureEntity> builder)
        {
            // Ignoring the Id property since the composite key is used
            builder.Ignore(x => x.Id);

            // Configuring a composite primary key using HotelId and FeatureId
            builder.HasKey("HotelId", "FeatureId");

            // Calls the base configuration to apply common configurations
            base.Configure(builder);
        }
    }
}