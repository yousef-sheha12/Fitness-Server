using Fitness.Data;
using Fitness.Interface.IRepository;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Repository
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly AppDbContext _context;

        public ConversationRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Conversation>> GetByUserIdAsync(int userId) =>
            await _context.Conversations
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .Include(c => c.User1)
                .Include(c => c.User2)
                .Include(c => c.Messages.OrderByDescending(m => m.CreatedAt).Take(1))
                .OrderByDescending(c => c.LastMessageAt)
                .ToListAsync();

        public async Task<Conversation?> GetByIdAsync(int id) =>
            await _context.Conversations
                .Include(c => c.User1)
                .Include(c => c.User2)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Conversation?> GetByUsersAsync(int user1Id, int user2Id) =>
            await _context.Conversations
                .FirstOrDefaultAsync(c =>
                    (c.User1Id == user1Id && c.User2Id == user2Id) ||
                    (c.User1Id == user2Id && c.User2Id == user1Id));

        public async Task<Conversation> CreateAsync(Conversation conversation)
        {
            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync();
            return conversation;
        }

        public async Task<bool> MarkAsReadAsync(int conversationId, int userId)
        {
            var messages = await _context.Messages
                .Where(m => m.ConversationId == conversationId && m.SenderId != userId && !m.IsRead)
                .ToListAsync();
            foreach (var msg in messages) msg.IsRead = true;
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
