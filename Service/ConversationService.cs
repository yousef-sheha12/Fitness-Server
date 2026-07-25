using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepo;
        private readonly IMessageRepository _messageRepo;

        public ConversationService(IConversationRepository conversationRepo, IMessageRepository messageRepo)
        {
            _conversationRepo = conversationRepo;
            _messageRepo = messageRepo;
        }

        public async Task<IEnumerable<Conversation>> GetByUserIdAsync(int userId) =>
            await _conversationRepo.GetByUserIdAsync(userId);

        public async Task<Conversation?> GetByIdAsync(int id) =>
            await _conversationRepo.GetByIdAsync(id);

        public async Task<Conversation> CreateAsync(int user1Id, int user2Id)
        {
            var existing = await _conversationRepo.GetByUsersAsync(user1Id, user2Id);
            if (existing != null) return existing;

            var conversation = new Conversation
            {
                User1Id = user1Id,
                User2Id = user2Id,
                CreatedAt = DateTime.UtcNow
            };
            return await _conversationRepo.CreateAsync(conversation);
        }

        public async Task<Message> SendMessageAsync(int conversationId, int senderId, string content)
        {
            var message = new Message
            {
                ConversationId = conversationId,
                SenderId = senderId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _messageRepo.CreateAsync(message);

            var conversation = await _conversationRepo.GetByIdAsync(conversationId);
            if (conversation != null)
            {
                conversation.LastMessageAt = DateTime.UtcNow;
                await _conversationRepo.CreateAsync(conversation);
            }

            return result;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(int conversationId) =>
            await _messageRepo.GetByConversationIdAsync(conversationId);

        public async Task<bool> MarkAsReadAsync(int conversationId, int userId) =>
            await _messageRepo.MarkAsReadAsync(conversationId, userId);
    }
}
