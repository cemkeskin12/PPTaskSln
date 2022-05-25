using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTask.Entity.DTOs
{
    public class InvoiceListDto
    {
        public double Debt { get; set; }
        public string Info { get; set; }
        public InvoiceTypeDto InvoiceType { get; set; }
        public SubscriberListDto Subscriber { get; set; }
    }
}
