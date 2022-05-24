using PPTask.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTask.Service.Services.Invoces
{
    public interface IInvoiceService
    {
        Task<List<Invoice>> ListAllInvoicesAsync();
    }
}
