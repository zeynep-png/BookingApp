using BookingApp.Data.Entities;
using BookingApp.Data.Repositories;
using BookingApp.Data.UnifOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Setting
{
    // SettingManager implements ISettingService to manage application settings
    public class SettingManager : ISettingService
    {
        private readonly IUnitOfWork _unitofWork; // Unit of work for managing database transactions
        private readonly IRepository<SettingEntity> _settingRepository; // Repository for accessing settings

        // Constructor to initialize the SettingManager with dependencies
        public SettingManager(IUnitOfWork unitofWork, IRepository<SettingEntity> settingRepository)
        {
            _unitofWork = unitofWork;
            _settingRepository = settingRepository;
        }

        // Gets the current state of maintenance mode
        public bool GetMaintenanceState()
        {
            // Fetches the maintenance state from the settings repository
            var maintenanceState = _settingRepository.GetById(1).MaintenanceMode;
            return maintenanceState;
        }

        // Toggles the maintenance mode state
        public async Task ToggleMaintenance()
        {
            // Retrieve the setting entity for maintenance mode
            var setting = _settingRepository.GetById(1);
            // Toggle the maintenance mode state
            setting.MaintenanceMode = !setting.MaintenanceMode;

            // Update the setting in the repository
            _settingRepository.Update(setting);

            try
            {
                // Save the changes to the database
                await _unitofWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Handle exceptions and throw a descriptive error
                throw new Exception("Error due maintenance");
            }
        }
    }
}
