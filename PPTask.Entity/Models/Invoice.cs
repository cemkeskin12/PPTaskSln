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
        public Invoice()
        {

        }
        public Invoice(double debt, string info, int invoiceTypeId, int subscriberId)
        {
            SetDebt(debt);
            SetInfo(info);
            SetInvoiceTypeId(invoiceTypeId);
            SetSubscriberId(subscriberId);
        }

        public void SetSubscriberId(int subscriberId)
        {
            SubscriberId = subscriberId;
        }

        public void SetInvoiceTypeId(int invoiceTypeId)
        {
            InvoiceTypeId = invoiceTypeId;
        }

        public void SetInfo(string info)
        {
            Info = info;
        }

        public void SetDebt(double debt)
        {
            Debt = debt;
        }

        public int Id { get; set; }
        public double Debt { get; set; }
        public string Info { get; set; }
        public int InvoiceTypeId { get; set; }
        public int SubscriberId { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}
