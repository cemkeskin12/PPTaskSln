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
        public Invoice(double debt,int invoiceTypeId, int subscriberId)
        {
            SetDebt(debt);
            Info = "Ödenmemiş";
            SetInvoiceTypeId(invoiceTypeId);
            SetSubscriberId(subscriberId);
        }

        public void SetSubscriberId(int subscriberId)
        {
            if (subscriberId <= 0)
                throw new Exception("Abone Numarası boş geçilmemelidir.");
            SubscriberId = subscriberId;
        }

        public void SetInvoiceTypeId(int invoiceTypeId)
        {
            if (invoiceTypeId <= 0)
                throw new Exception("Fatura Tipi boş geçilmemelidir.");
            InvoiceTypeId = invoiceTypeId;
        }
        public void SetDebt(double debt)
        {
            if (debt < 0)
            {
                throw new Exception("Fatura, 0'dan küçük olamaz.");
            }
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
