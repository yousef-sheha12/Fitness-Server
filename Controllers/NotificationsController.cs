using Fitness.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.Controllers
{
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        private int? GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : null;
        }

        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

        [HttpGet("api/notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var notifications = await _notificationService.GetByUserIdAsync(userId.Value);
            return ApiResponse(notifications);
        }

        [HttpPatch("api/notifications/{id}/mark-read")]
        public async Task<IActionResult> MarkNotificationRead(int id)
        {
            var result = await _notificationService.MarkAsReadAsync(id);
            if (!result) return ApiResponse(null, "Notification not found", 404);
            return ApiResponse(message: "Notification marked as read");
        }

        [HttpPatch("api/notifications/mark-all-read")]
        public async Task<IActionResult> MarkAllNotificationsRead()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            await _notificationService.MarkAllAsReadAsync(userId.Value);
            return ApiResponse(message: "All notifications marked as read");
        }

        [HttpDelete("api/notifications/{id}/delete")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var result = await _notificationService.DeleteAsync(id);
            if (!result) return ApiResponse(null, "Notification not found", 404);
            return ApiResponse(message: "Notification deleted");
        }
    }
}
