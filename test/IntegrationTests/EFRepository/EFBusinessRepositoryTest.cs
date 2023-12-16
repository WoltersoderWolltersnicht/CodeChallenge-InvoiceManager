using FluentAssertions;
using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using InvoiceManager.Domain.People;
using InvoiceManager.Infrastructure.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.EFRepository;

public class EFBusinessRepositoryTest : IAsyncLifetime
{
    private string _connectionString;

    [Fact]
    public async Task Create()
    {
        EFBusinessRepository businessRepository = new(DbContextBuilder.Build(_connectionString));

        var createResult = await businessRepository.Create(new Business()
        {
            CIF = "test",
            Invoices = new List<Invoice>() { },
            Name = "test"
        });
        createResult.IsSuccess.Should().BeTrue();

        var context = DbContextBuilder.Build(_connectionString);

        var storedBusiness = context.Business.Include(b => b.Invoices).ThenInclude(i => i.InvoiceLines)
            .Include(b => b.Invoices).ThenInclude(i => i.Person).FirstOrDefault(b => b.Id == createResult.Value.Id);

        storedBusiness.Should().NotBeNull();
        storedBusiness.Should().BeEquivalentTo(createResult.Value);
    }

    [Fact]
    public async Task Read()
    {
        MockMySqlConnection connection = await MockMySqlConnection.CreateAsync();

        EFBusinessRepository businessRepository = new(DbContextBuilder.Build(_connectionString));

        var readResponse = await businessRepository.GetById(1);

        readResponse.IsSuccess.Should().BeTrue();
        readResponse.Value.Should().NotBeNull();
        readResponse.Value.CIF.Should().Be("testCIF1");
        readResponse.Value.Name.Should().Be("testName1");
        readResponse.Value.Invoices.Should().NotBeNullOrEmpty();
        readResponse.Value.Invoices.First().Estado.Should().Be(InvoiceStatusEnum.New);
        readResponse.Value.Invoices.First().Person.Should().NotBeNull();
        readResponse.Value.Invoices.First().InvoiceLines.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Update()
    {
        MockMySqlConnection connection = await MockMySqlConnection.CreateAsync();

        EFBusinessRepository businessRepository = new(DbContextBuilder.Build(_connectionString));

        var updateResult = await businessRepository.Update(new Business()
        {
            Id = 2,
            CIF = "testUpdate",
            Name = "testUpdate"
        });

        updateResult.IsSuccess.Should().BeTrue();
        updateResult.Value.Should().NotBeNull();

        var context = DbContextBuilder.Build(_connectionString);
        var updatedBusiness = context.Business.FirstOrDefault(b => b.Id == 2);

        updatedBusiness.CIF.Should().Be("testUpdate");
        updatedBusiness.Name.Should().Be("testUpdate");
    }

    [Fact]
    public async Task Delete()
    {
        EFBusinessRepository businessRepository = new(DbContextBuilder.Build(_connectionString));

        var updateResult = await businessRepository.Delete(new Business()
        {
            Id = 3
        });

        updateResult.IsSuccess.Should().BeTrue();

        var context = DbContextBuilder.Build(_connectionString);
        var nullDeletedValue = context.Business.FirstOrDefault(b => b.Id == 3);
        nullDeletedValue.Should().BeNull();
    }

    private async Task StoreBusiness()
    {
        var context = DbContextBuilder.Build(_connectionString);

        context.Business.AddRange(new[]
        {
        new Business()
        {
            CIF = "testCIF1",
            Name = "testName1",
            Invoices = new List<Invoice>() {
                new Invoice(){
                    GUID = "testGuid1",
                    Amount = 50,
                    VAT = 50,
                    Estado = InvoiceStatusEnum.New,
                    Number = "testNumber1",
                    Person = new Person()
                    {
                        Name = "testName",
                        NIF = "testNif1",
                        Surname1 = "testSurname1",
                        Surname2 = "testSurname2",
                    },
                    InvoiceLines = new List<InvoiceLine>()
                    {
                        new InvoiceLine()
                        {
                            VAT = 50,
                            Amount = 50,
                        }
                    },
                }
            }

        },
        new Business()
        {
            CIF = "testCIF2",
            Name = "testName2",
            Invoices = new List<Invoice>() {
                new Invoice(){
                    GUID = "testGuid2",
                    Amount = 50,
                    VAT = 50,
                    Estado = InvoiceStatusEnum.New,
                    Number = "testNumber2",
                    Person = new Person()
                    {
                        Name = "testName",
                        NIF = "testNif2",
                        Surname1 = "testSurname1",
                        Surname2 = "testSurname2",
                    },
                    InvoiceLines = new List<InvoiceLine>()
                    {
                        new InvoiceLine()
                        {
                            VAT = 50,
                            Amount = 50,
                        }
                    },
                }
            }
        },
        new Business()
        {
            CIF = "testCIF3",
            Name = "testName3",
            Invoices = new List<Invoice>() {
                new Invoice(){
                    GUID = "testGuid3",
                    Amount = 50,
                    VAT = 50,
                    Estado = InvoiceStatusEnum.New,
                    Number = "testNumber3",
                    Person = new Person()
                    {
                        Name = "testName",
                        NIF = "testNif3",
                        Surname1 = "testSurname1",
                        Surname2 = "testSurname2",
                    },
                    InvoiceLines = new List<InvoiceLine>()
                    {
                        new InvoiceLine()
                        {
                            VAT = 50,
                            Amount = 50,
                        }
                    },
                }
            }
        }
        });

        await context.SaveChangesAsync();

    }

    public async Task InitializeAsync()
    {
        MockMySqlConnection connection = await MockMySqlConnection.CreateAsync();
        _connectionString = connection.ConnectionString;
        await StoreBusiness();
    }

    public async Task DisposeAsync()
    {
        return;
    }
}