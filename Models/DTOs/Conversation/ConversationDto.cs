namespace Fitness.Models.DTOs.Conversation
{
    public class CreateConversationDto
    {
        public int ReceiverId { get; set; }
    }

    public class SendMessageDto
    {
        public string Content { get; set; } = string.Empty;
    }
}
