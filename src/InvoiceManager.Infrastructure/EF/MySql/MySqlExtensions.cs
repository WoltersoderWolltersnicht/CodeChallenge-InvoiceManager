using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
}
