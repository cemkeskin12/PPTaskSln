using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTask.Entity.DTOs
{
    public class InvoiceTypeDto
    {
        public string Name { get; set; }
        public ICollection<InvoiceDto> Invoices { get; set; }
    }
}
