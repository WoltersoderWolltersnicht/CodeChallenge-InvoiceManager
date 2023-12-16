using FluentAssertions;
using InvoiceManager.Application.Handler.People.GetPerson;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using InvoiceManager.Domain.People;
using NSubstitute;

namespace UnitTests.Handler.People;

public class GetPersonHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.GetById(default).ReturnsForAnyArgs(x => new Person()
        {
            Id = (uint)x[0],
            Name = "testname",
            NIF = "testNif",
            Surname1 = "testSurname1",
            Surname2 = "testSurname2",
            Invoices = new List<Invoice>()
        });

        GetPersonQueryHandler handler = new(personRepository);
        GetPersonQuery command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Person.NIF.Should().Be("testNif");
        response.Value.Person.Name.Should().Be("testname");
        response.Value.Person.Surname1.Should().Be("testSurname1");
        response.Value.Person.Surname2.Should().Be("testSurname2");
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));

        GetPersonQueryHandler handler = new(personRepository);
        GetPersonQuery command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
