using Fitness.Models;

namespace Fitness.Interface.IRepository
{
    public interface IConversationRepository
    {
        Task<IEnumerable<Conversation>> GetByUserIdAsync(int userId);
        Task<Conversation?> GetByIdAsync(int id);
        Task<Conversation?> GetByUsersAsync(int user1Id, int user2Id);
        Task<Conversation> CreateAsync(Conversation conversation);
        Task<bool> MarkAsReadAsync(int conversationId, int userId);
    }
}
