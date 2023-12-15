using FluentAssertions;
using InvoiceManager.Application.Handler.Businesses.UpdateBusiness;
using InvoiceManager.Application.Handler.People.UpdatePerson;
using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using InvoiceManager.Domain.People;
using NSubstitute;

namespace UnitTests.Handler.Businesses.UpdateBusiness;

public class UpdatePersonHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.Filter(default).ReturnsForAnyArgs(x => new List<Person>());
        personRepository.GetById(default).ReturnsForAnyArgs(x => new Person()
        {
            Id = (uint)x[0],
            Name = "testname",
            NIF = "testNif",
            Surname1 = "testSurname1",
            Surname2 = "testSurname2",
            Invoices = new List<Invoice>()
        });
        personRepository.Update(default).ReturnsForAnyArgs(x => (Person)x[0]);

        UpdatePersonQueryHandler handler = new(personRepository);
        UpdatePersonCommand command = new(1, "updatedName", "updatedSurname1", "updatedSurname1", "updatedNif");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Person.Id.Should().Be(1);
        response.Value.Person.Name.Should().Be(command.Name);
        response.Value.Person.Surname1.Should().Be(command.Surname1);
        response.Value.Person.Surname2.Should().Be(command.Surname2);
        response.Value.Person.NIF.Should().Be(command.NIF);
    }

    [Fact]
    public async Task HandleDuplicateCIFOrName()
    {
        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.Filter(default).ReturnsForAnyArgs(x => new List<Person>() { new Person() });
        personRepository.GetById(default).ReturnsForAnyArgs(x => new Person()
        {
            Id = (uint)x[0],
            Name = "testname",
            NIF = "testNif",
            Surname1 = "testSurname1",
            Surname2 = "testSurname2",
            Invoices = new List<Invoice>()
        });
        personRepository.Update(default).ReturnsForAnyArgs(x => (Person)x[0]);

        UpdatePersonQueryHandler handler = new(personRepository);
        UpdatePersonCommand command = new(1, "updatedName", "updatedSurname1", "updatedSurname1", "updatedNif");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(DupplicateKeyException));
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.Filter(default).ReturnsForAnyArgs(x => new List<Business>() { });
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));
        businessRepository.Update(default).ReturnsForAnyArgs(x => (Business)x[0]);

        UpdateBusinessCommandHandler handler = new UpdateBusinessCommandHandler(businessRepository);
        UpdateBusinessCommand command = new(1, "updatedName", "updatedCIF");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
