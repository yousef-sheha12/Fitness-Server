using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface IConversationService
    {
        Task<IEnumerable<Conversation>> GetByUserIdAsync(int userId);
        Task<Conversation?> GetByIdAsync(int id);
        Task<Conversation> CreateAsync(int user1Id, int user2Id);
        Task<Message> SendMessageAsync(int conversationId, int senderId, string content);
        Task<IEnumerable<Message>> GetMessagesAsync(int conversationId);
        Task<bool> MarkAsReadAsync(int conversationId, int userId);
    }
}
