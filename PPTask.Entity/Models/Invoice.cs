using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTask.Entity.Models
{
    //fatura
    public class Invoice
    {
        public int Id { get; set; }
        public double Debt { get; set; }
        public string Info { get; set; }
        public int InvoiceTypeId { get; set; }
        public int SubscriberId { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}
