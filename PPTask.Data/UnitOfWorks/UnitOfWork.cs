using PPTask.Core.Interfaces.Repositories;
using PPTask.Data.Context;
using PPTask.Data.Repositories;
using PPTask.Entity.Models;

namespace PPTask.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private IRepository<Subscriber> subscriberRepository;
        private IRepository<Invoice> invoiceRepository;
        private IRepository<InvoiceType> invoiceTypeRepository;
        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IRepository<Subscriber> Subscribers => subscriberRepository ?? new Repository<Subscriber>(dbContext);
        public IRepository<Invoice> Invoices => invoiceRepository ?? new Repository<Invoice>(dbContext);
        public IRepository<InvoiceType> InvoiceTypes => invoiceTypeRepository ?? new Repository<InvoiceType>(dbContext);
        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }

        public int Save()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
