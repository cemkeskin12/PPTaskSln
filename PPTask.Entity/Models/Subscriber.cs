using PPTask.Core.Interfaces.Entities;

namespace PPTask.Entity.Models
{
    public class Subscriber : IEntityBase
    {
        public Subscriber()
        {

        }
        public Subscriber(string firstName, string lastName, double deposit)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetDeposit(deposit);
            State = true;

        }

        public void SetDeposit(double deposit)
        {
            if(deposit <= 0)
                throw new Exception("Hatalı depozito değeri girildi!");
            Deposit = deposit;
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new Exception("Abonenin soyadının girilmesi zorunludur!");
            LastName = lastName;
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new Exception("Abonenin isminin girilmesi zorunludur!");
            FirstName = firstName;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Deposit { get; set; }
        public bool State { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
