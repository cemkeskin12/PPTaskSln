using PPTask.Entity.Models;

namespace PPTask.Service.Services.Subscribers
{
    public interface ISubscriberService
    {
        Task<List<Subscriber>> ListSubscriberByIdAsync(int id);
    }
}
