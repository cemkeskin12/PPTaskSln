using PPTask.Core.Interfaces.Entities;

namespace PPTask.Entity.Models
{
    public class Subscriber : IEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Deposit { get; set; }
        public bool State { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
