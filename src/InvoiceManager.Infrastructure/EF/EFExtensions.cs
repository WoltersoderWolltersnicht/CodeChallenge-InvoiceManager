using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using InvoiceManager.Domain.People;
using InvoiceManager.Infrastructure.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceManager.Infrastructure.EF;

public static class EFExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, EFPersonRepository>();

        services.AddScoped<IBusinessRepository, EFBusinessRepository>();

        services.AddScoped<IInvoiceLineRepository, EFInvoiceLineRepository>();

        services.AddScoped<IInvoiceRepository, EFInvoiceRepository>();

        return services;
    }
}
