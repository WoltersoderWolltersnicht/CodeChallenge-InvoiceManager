using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InvoiceManager.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using InvoiceManager.Domain.People;
using InvoiceManager.Infrastructure.EF.MySql.Repositories;
using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;

namespace InvoiceManager.Infrastructure.EF.MySql;

public static class MySqlExtensions
{
    public static IServiceCollection UseMySqlDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        MySqlConfiguration mySqlConfiguration = new();
        configuration.Bind("MySql", mySqlConfiguration);

        services.AddDbContext<InvoiceManagerDbContext>(options =>
        {
            var connectionString = mySqlConfiguration.BuildConnectionString();
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        
        return services;
    }

    public static IServiceCollection UseMySqlRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IPersonRepository, MySqlPersonRepository>();

        services.AddSingleton<IBusinessRepository, MySqlBusinessRepository>();

        services.AddSingleton<IInvoiceLineRepository, MySqlInvoiceLineRepository>();

        services.AddSingleton<IInvoiceRepository, MySqlInvoiceRepository>();

        return services;
    }
}
