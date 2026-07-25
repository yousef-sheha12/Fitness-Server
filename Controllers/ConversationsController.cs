using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

        [HttpGet("api/conversations")]
        public IActionResult GetConversations()
        {
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPost("api/conversations")]
        public IActionResult CreateConversation([FromBody] object dto)
        {
            return ApiResponse(message: "Conversation created");
        }

        [HttpGet("api/conversations/{id}/messages")]
        public IActionResult GetMessages(int id)
        {
            return ApiResponse(Array.Empty<object>());
        }

        [HttpPost("api/conversations/{id}/messages")]
        public IActionResult SendMessage(int id, [FromBody] object dto)
        {
            return ApiResponse(message: "Message sent");
        }

        [HttpPatch("api/conversations/{id}/read")]
        public IActionResult MarkConversationRead(int id)
        {
            return ApiResponse(message: "Conversation marked as read");
        }
    }
}
