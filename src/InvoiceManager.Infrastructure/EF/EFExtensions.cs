using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using InvoiceManager.Domain.People;
using InvoiceManager.Infrastructure.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

    public static IServiceProvider EnsureEFConnection(this IServiceProvider services)
    {
        //ILogger logger = services.GetRequiredService<ILogger>();
        var dbContextFactory = services.GetRequiredService<IDbContextFactory<InvoiceManagerDbContext>>();

        var dbContext = dbContextFactory.CreateDbContext();

        var isCreated = dbContext.Database.EnsureCreated();
        //if (isCreated) logger.LogInformation("Database Created");

        var canConnect = dbContext.Database.CanConnect();
        if(!canConnect) throw new Exception("Database not created succesfully");

        //logger.LogInformation("Connected to Database Succesfuly");

        return services;
    }
}
