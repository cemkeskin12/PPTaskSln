using PPTask.Data.UnitOfWorks;
using PPTask.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTask.Service.Services.Subscribers
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IUnitOfWork unitOfWork;

        public SubscriberService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Subscriber>> ListSubscriberByIdAsync(int id)
        {
            return await unitOfWork.Subscribers.GetAllAsync(x=>x.Id == id && x.State != false, x=>x.Invoices);
        }
    }
}
