using AutoMapper;
using PPTask.Data.UnitOfWorks;
using PPTask.Entity.DTOs;

namespace PPTask.Service.Services.Invoces
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<List<InvoiceListDto>> ListAllInvoicesAsync()
        {
            var invoices = await unitOfWork.Invoices.GetAllAsync(null, x => x.InvoiceType, x => x.Subscriber);
            return mapper.Map<List<InvoiceListDto>>(invoices);
        }
        public async Task<double> PayInvoice(InvoicePayDto invoicePayDto)
        {
            var invoice = await unitOfWork.Invoices.GetByIdAsync(invoicePayDto.Id);
            if (invoicePayDto.Debt <= 0)
                throw new Exception("Ödenen miktar 0 ve 0'dan küçük olamaz!");
            if (invoicePayDto.Debt <= invoice.Debt)
            {
                invoice.Debt -= invoicePayDto.Debt;
                invoice.SetDebt(invoice.Debt);
                if(invoice.Debt == 0)
                    invoice.Info = "Ödenmiş";
                unitOfWork.Invoices.Update(invoice);
                await unitOfWork.SaveAsync();
                return invoice.Debt;
            }
            throw new Exception("Ödenen miktar, fatura tutarından büyük olamaz!");
        }
        
    }
}
