using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Setting
{
    // ISettingService defines operations related to application settings
    public interface ISettingService
    {
        // Toggles the maintenance mode on or off
        Task ToggleMaintenance();

        // Retrieves the current state of maintenance mode (true if in maintenance, false otherwise)
        bool GetMaintenanceState();
    }
}
