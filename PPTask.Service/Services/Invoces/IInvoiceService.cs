using PPTask.Entity.DTOs;

namespace PPTask.Service.Services.Invoces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceListDto>> ListAllInvoicesAsync();
        Task<double> PayInvoice(InvoicePayDto invoicePayDto);
    }
}
