using Fitness.Interface.IService;
using Fitness.Models.DTOs.Conversation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitness.Controllers
{
    [ApiController]
    [Authorize]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationsController(IConversationService conversationService)
        {
            _conversationService = conversationService;
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

        [HttpGet("api/conversations")]
        public async Task<IActionResult> GetConversations()
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var conversations = await _conversationService.GetByUserIdAsync(userId.Value);
            return ApiResponse(conversations);
        }

        [HttpPost("api/conversations")]
        public async Task<IActionResult> CreateConversation([FromBody] CreateConversationDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var conversation = await _conversationService.CreateAsync(userId.Value, dto.ReceiverId);
            return ApiResponse(conversation, "Conversation created");
        }

        [HttpGet("api/conversations/{id}/messages")]
        public async Task<IActionResult> GetMessages(int id)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var messages = await _conversationService.GetMessagesAsync(id);
            return ApiResponse(messages);
        }

        [HttpPost("api/conversations/{id}/messages")]
        public async Task<IActionResult> SendMessage(int id, [FromBody] SendMessageDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            var message = await _conversationService.SendMessageAsync(id, userId.Value, dto.Content);
            return ApiResponse(message, "Message sent");
        }

        [HttpPatch("api/conversations/{id}/read")]
        public async Task<IActionResult> MarkConversationRead(int id)
        {
            var userId = GetUserId();
            if (userId == null) return ApiResponse(null, "Unauthorized", 401);
            await _conversationService.MarkAsReadAsync(id, userId.Value);
            return ApiResponse(message: "Conversation marked as read");
        }
    }
}
