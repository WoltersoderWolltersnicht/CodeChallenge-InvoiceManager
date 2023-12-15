using FluentAssertions;
using InvoiceManager.Application.Handler.Businesses.CreateBusiness;
using InvoiceManager.Application.Handler.People.CreatePerson;
using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.People;
using NSubstitute;

namespace UnitTests.Handler.Businesses;

public class CreatePersonLineHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.Filter(default).ReturnsForAnyArgs(new List<Person>());
        personRepository.Create(default).ReturnsForAnyArgs(x => (Person)x[0]);

        CreatePersonCommandHandler handler = new(personRepository);
        CreatePersonCommand command = new("TestNif", "TestName", "TestSurname1", "TestSurname2");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Person.NIF.Should().Be(command.NIF);
        response.Value.Person.Name.Should().Be(command.Name);
        response.Value.Person.Surname1.Should().Be(command.Surname1);
        response.Value.Person.Surname2.Should().Be(command.Surname2);
    }

    [Fact]
    public async Task HandleDuplicateCIFOrName()
    {
        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.Filter(default).ReturnsForAnyArgs(new List<Person>() { new Person() });
        personRepository.Create(default).ReturnsForAnyArgs(x => (Person)x[0]);

        CreatePersonCommandHandler handler = new(personRepository);
        CreatePersonCommand command = new("TestNif", "TestName", "TestSurname1", "TestSurname2");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(DupplicateKeyException));
    }
}
