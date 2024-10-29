using BookingApp.Business.Operations.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService; // Service for handling settings operations

        // Constructor accepting the setting service via dependency injection
        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService; // Initialize the setting service
        }

        // Endpoint for toggling maintenance mode
        [HttpPatch] // This method will respond to PATCH requests
        public async Task<IActionResult> ToggleMaintenence()
        {
            await _settingService.ToggleMaintenance(); // Call the service to toggle maintenance mode
            return Ok(); // Return OK response after successful operation
        }
    }
}