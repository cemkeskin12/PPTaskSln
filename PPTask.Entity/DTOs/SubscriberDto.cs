using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTask.Entity.DTOs
{
    public class SubscriberDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Deposit { get; set; }
        public bool State { get; set; }
        public ICollection<InvoiceDto> Invoices { get; set; }

    }
}
