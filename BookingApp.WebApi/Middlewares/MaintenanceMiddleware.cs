using BookingApp.Business.Operations.Setting;

namespace BookingApp.WebApi.Middlewares
{
    public class MaintenanceMiddleware // Middleware to handle maintenance mode
    {
        private readonly RequestDelegate _next; // Delegate to reference the next middleware in the pipeline

        public MaintenanceMiddleware(RequestDelegate next) // Constructor to initialize the middleware
        {
            _next = next; // Store the next middleware for invocation later
        }

        public async Task Invoke(HttpContext context) // Method to process the HTTP request
        {
            // Get the setting service from the request's service provider
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();

            // Check if the maintenance mode is enabled
            bool maintenanceMode = settingService.GetMaintenanceState();
            if (maintenanceMode) // If in maintenance mode
            {
                // Respond with a maintenance message
                await context.Response.WriteAsync("Maintenance mode on. Try another time.");
            }
            else // If not in maintenance mode
            {
                await _next(context); // Call the next middleware in the pipeline
            }
        }
    }
}