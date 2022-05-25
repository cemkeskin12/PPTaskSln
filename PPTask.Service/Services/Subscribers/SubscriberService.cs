using AutoMapper;
using PPTask.Data.UnitOfWorks;
using PPTask.Entity.DTOs;
using PPTask.Entity.Models;

namespace PPTask.Service.Services.Subscribers
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SubscriberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<SubscriberDto>> ListSubscriberByIdAsync(int id)
        {
            var subs = await unitOfWork.Subscribers.GetAllAsync(x => x.Id == id && x.State != false, x => x.Invoices);
            return mapper.Map<List<SubscriberDto>>(subs);
        }
        public async Task AddSubscriber(SubscriberAddDto subscriberAddDto)
        {
            var subs = new Subscriber(
                subscriberAddDto.FirstName,
                subscriberAddDto.LastName,
                subscriberAddDto.Deposit
                );
            await unitOfWork.Subscribers.AddAsync(subs);
            await unitOfWork.SaveAsync();
        }
        public async Task DeleteSubscriber(SubscriberDeleteDto subscriberDeleteDto)
        {
            var subs = await unitOfWork.Subscribers.GetAllAsync(x=>x.Id == subscriberDeleteDto.Id,x=>x.Invoices);
            foreach (var item in subs)
            {
                if (item.Invoices.Any(x => x.Debt > 0))
                    throw new Exception("Ödenmemiş Fatura Bulunmaktadır.");
                item.State = false;
                item.Deposit = 0;
                unitOfWork.Subscribers.Update(item);
                await unitOfWork.SaveAsync();
            }

        }
    }
}
