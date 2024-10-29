namespace BookingApp.WebApi.Middlewares
{
    
        public static class MiddlewareExtensions // Static class for middleware extension methods
        {
            // Extension method to add MaintenanceMiddleware to the application builder
            public static IApplicationBuilder UseMaintenanceMode(this IApplicationBuilder app)
            {
                return app.UseMiddleware<MaintenanceMiddleware>(); // Register the MaintenanceMiddleware
            }
        }
    }