using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

        [HttpGet("api/notifications")]
        public IActionResult GetNotifications()
        {
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPatch("api/notifications/{id}/mark-read")]
        public IActionResult MarkNotificationRead(int id)
        {
            return ApiResponse(message: "Notification marked as read");
        }

        [HttpPatch("api/notifications/mark-all-read")]
        public IActionResult MarkAllNotificationsRead()
        {
            return ApiResponse(message: "All notifications marked as read");
        }

        [HttpDelete("api/notifications/{id}/delete")]
        public IActionResult DeleteNotification(int id)
        {
            return ApiResponse(message: "Notification deleted");
        }
    }
}
