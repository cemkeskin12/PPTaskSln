using PPTask.Data.UnitOfWorks;
using PPTask.Entity.DTOs;
using PPTask.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTask.Service.Services.Invoces
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<List<InvoiceDto>> ListAllInvoicesAsync()
        {
            return unitOfWork.Invoices.GetAllAsync(null, x => x.InvoiceType,x=>x.Subscriber);
        }
        //public async Task<List<Invoice>> PayInvoice(int id, double pay)
        //{
        //}
    }
}
