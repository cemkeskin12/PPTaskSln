using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PPTask.Entity.Models;
using PPTask.Service.Services.Invoces;

namespace PPTask.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [HttpPost]
        public async Task<IActionResult> PayInvoice(int id, double pay)
        {
            var result = invoiceService.PayInvoice(id, pay);
            return Ok(result);
        }
    }
}
