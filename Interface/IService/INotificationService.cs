using Fitness.Models;

namespace Fitness.Interface.IService
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
        Task<Notification> CreateAsync(Notification notification);
        Task<bool> MarkAsReadAsync(int id);
        Task<bool> MarkAllAsReadAsync(int userId);
        Task<bool> DeleteAsync(int id);
    }
}
