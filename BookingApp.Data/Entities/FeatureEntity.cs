using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BookingApp.Data.Entities
{
    // Entity class representing a feature associated with hotels
    public class FeatureEntity : BaseEntity
    {
        // The title or name of the feature (e.g., "Free WiFi", "Swimming Pool")
        public string Title { get; set; }

        // Navigation property representing the relationship between features and hotel features
        public ICollection<HotelFeatureEntity> HotelFeatures { get; set; }
    }

    // Configuration class for FeatureEntity using fluent API
    public class FeatureConfiguration : BaseConfiguration<FeatureEntity>
    {
        // Overrides the Configure method to apply additional configurations if needed
        public override void Configure(EntityTypeBuilder<FeatureEntity> builder)
        {
            // Calls the base configuration to apply common configurations
            base.Configure(builder);

            // Additional configuration can be added here if needed
        }
    }
}
