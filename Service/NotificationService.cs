using Fitness.Interface.IRepository;
using Fitness.Interface.IService;
using Fitness.Models;

namespace Fitness.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo) => _repo = repo;

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(int userId) =>
            await _repo.GetByUserIdAsync(userId);

        public async Task<Notification> CreateAsync(Notification notification) =>
            await _repo.CreateAsync(notification);

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var notification = await _repo.GetByIdAsync(id);
            if (notification == null) return false;
            notification.IsRead = true;
            return await _repo.UpdateAsync(notification);
        }

        public async Task<bool> MarkAllAsReadAsync(int userId)
        {
            var notifications = await _repo.GetByUserIdAsync(userId);
            foreach (var n in notifications.Where(n => !n.IsRead))
            {
                n.IsRead = true;
                await _repo.UpdateAsync(n);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);
    }
}
