using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Entities
{
    // Entity class representing application settings
    public class SettingEntity : BaseEntity
    {
        // Indicates whether the application is in maintenance mode
        public bool MaintenanceMode { get; set; }
    }
}
