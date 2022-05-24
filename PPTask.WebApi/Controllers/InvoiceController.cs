using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPTask.Service.Services.Invoces;

namespace PPTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await invoiceService.ListAllInvoicesAsync();
            return Ok(invoices);
        }
    }
}
