using InvoiceManager.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MySql;

namespace IntegrationTests;

public class MockMySqlConnection
{
    private string _databaseName = "test";
    private string _userName = "root";
    private string _password = "test";
    public string ConnectionString { get; set; }

    public static async Task<MockMySqlConnection> CreateAsync()
    {
        var instance = new MockMySqlConnection();
        await instance.Connect();
        return instance;
    }

    private async Task Connect()
    {
        var mySqlContainter = new MySqlBuilder()
                              .WithImage("mysql:latest")
                              .WithDatabase(_databaseName)
                              .WithUsername(_userName)
                              .WithPassword(_password)
                              .Build();

        await mySqlContainter.StartAsync();

        InvoiceManager.Infrastructure.EF.MySql.MySqlConfiguration mySqlConfiguration = new()
        {
            DatabaseName = _databaseName,
            Password = _password,
            Port = mySqlContainter.GetMappedPublicPort(3306).ToString(),
            Url = mySqlContainter.Hostname,
            User = _userName
        };

        ConnectionString = mySqlConfiguration.BuildConnectionString();
    }
}

public static class DbContextBuilder
{
    public static InvoiceManagerDbContext Build(string connectionString)
    {
        var contextOptions = new DbContextOptionsBuilder<InvoiceManagerDbContext>()
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .Options;

        InvoiceManagerDbContext context = new(contextOptions);

        context.Database.EnsureCreated();

        return context;
    }
}
