using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PPTask.Core.Interfaces.Repositories;
using PPTask.Data.Context;
using PPTask.Data.Repositories;
using PPTask.Data.UnitOfWorks;
using PPTask.Service.Services.Invoces;
using PPTask.Service.Services.Subscribers;

namespace PPTask.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadCustomServices(this IServiceCollection services, IConfiguration config)
        {
            

        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISubscriberService, SubscriberService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            return services;
        }
    }
}
