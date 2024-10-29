using BookingApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Context
{
    // DbContext class for managing the BookingApp database
    public class BookingAppDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes them to the base DbContext
        public BookingAppDbContext(DbContextOptions<BookingAppDbContext> options) : base(options)
        {
        }

        // Configures the model with Fluent API and seeds initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Applying configurations for various entities using Fluent API
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new HotelFeatureConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            // Seeding initial data for the SettingEntity
            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenanceMode = false // Initial setting for maintenance mode
                });

            // Calling the base implementation to ensure proper configuration
            base.OnModelCreating(modelBuilder);
        }

        // DbSet properties representing tables in the database
        public DbSet<UserEntity> Users => Set<UserEntity>(); // Table for user entities
        public DbSet<FeatureEntity> Features => Set<FeatureEntity>(); // Table for feature entities
        public DbSet<HotelEntity> Hotels => Set<HotelEntity>(); // Table for hotel entities
        public DbSet<HotelFeatureEntity> HotelFeatures => Set<HotelFeatureEntity>(); // Table for hotel-feature relationships
        public DbSet<ReservationEntity> Reservations => Set<ReservationEntity>(); // Table for reservation entities
        public DbSet<RoomEntity> Rooms => Set<RoomEntity>(); // Table for room entities
        public DbSet<SettingEntity> Settings => Set<SettingEntity>(); // Table for settings
    }
}
