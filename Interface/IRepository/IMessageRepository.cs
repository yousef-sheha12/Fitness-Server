using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetByConversationIdAsync(int conversationId);
        Task<Message> CreateAsync(Message message);
        Task<bool> MarkAsReadAsync(int conversationId, int userId);
    }
}
