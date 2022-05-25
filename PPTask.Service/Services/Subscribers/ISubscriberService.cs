using PPTask.Entity.DTOs;
using PPTask.Entity.Models;

namespace PPTask.Service.Services.Subscribers
{
    public interface ISubscriberService
    {
        Task<List<SubscriberDto>> ListSubscriberByIdAsync(int id);
        Task AddSubscriber(SubscriberAddDto subscriberAddDto);
        Task DeleteSubscriber(SubscriberDeleteDto subscriberDeleteDto);
    }
}
