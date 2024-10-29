using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookingApp.WebApi.Filters
{
    // Custom filter to control access based on specified time range
    public class TimeControlFilter : ActionFilterAttribute
    {
        // Start and end time properties for allowed access time range
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        // Overrides the OnActionExecuted method, which is executed after the action method is called
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Get the current time of day
            var now = DateTime.Now.TimeOfDay;

            // Define access time range
            StartTime = "23.00";
            EndTime = "23.59";

            // Check if the current time is within the allowed time range
            if (now >= TimeSpan.Parse(StartTime) && now <= TimeSpan.Parse(EndTime))
            {
                // Proceed with the action execution if within allowed time range
                base.OnActionExecuted(context);
            }
            else
            {
                // Restrict access by returning a 403 Forbidden status with a custom message
                context.Result = new ContentResult
                {
                    Content = "You can not use any end-point now",
                    StatusCode = 403
                };
            }
        }
    }
}
