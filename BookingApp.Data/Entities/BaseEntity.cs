using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Entities
{
    // Base class for all entities in the application
    public class BaseEntity
    {
        // Unique identifier for the entity
        public int Id { get; set; }

        // Timestamp for when the entity was created
        public DateTime CreatedDate { get; set; }

        // Timestamp for when the entity was last modified (nullable)
        public DateTime? ModifiedDate { get; set; }

        // Flag to indicate if the entity is deleted (soft delete)
        public bool IsDeleted { get; set; }
    }

    // Abstract base configuration class for entity configurations
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity // Constraint to ensure TEntity is a BaseEntity
    {
        // Configures the entity using the EntityTypeBuilder
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Applies a global query filter to exclude soft-deleted entities from queries
            builder.HasQueryFilter(x => x.IsDeleted == false);

            // Configures the ModifiedDate property to be optional
            builder.Property(x => x.ModifiedDate)
                .IsRequired(false);
        }
    }
}
