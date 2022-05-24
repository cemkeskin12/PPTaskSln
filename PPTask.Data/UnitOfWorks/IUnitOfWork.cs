using PPTask.Core.Interfaces.Repositories;
using PPTask.Entity.Models;

namespace PPTask.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<Subscriber> Subscribers { get; }
        IRepository<Invoice> Invoices { get; }
        IRepository<InvoiceType> InvoiceTypes { get; }
        Task<int> SaveAsync();
        int Save();
    }
}
