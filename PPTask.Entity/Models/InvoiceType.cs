using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTask.Entity.Models
{
    public class InvoiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
