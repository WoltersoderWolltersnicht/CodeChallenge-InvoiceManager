using FluentAssertions;
using InvoiceManager.Application.Handler.Invoices.CreateInvoice;
using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using InvoiceManager.Domain.People;
using NSubstitute;

namespace UnitTests.Handler.Invoices;

public class CreateInvoiceHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.Create(default).ReturnsForAnyArgs(x => (Invoice)x[0]);

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
        personRepository.Create(default).ReturnsForAnyArgs(x => (Person)x[0]);

        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new Business()
        {
            Id = (uint)x[0],
            Name = "testname",
            CIF = "testcif",
            Invoices = new List<Invoice>()
        });

        CreateInvoiceCommandHandler handler = new(invoiceRepository, personRepository, businessRepository);
        CreateInvoiceCommand command = new("testnumber", 0, 0, InvoiceStatusEnum.New, new List<InvoiceLineDto>(), 5, new PersonDto(1, null, null, null, null));

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task HandleBusinessNotFound()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.Create(default).ReturnsForAnyArgs(x => (Invoice)x[0]);

        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.GetById(default).ReturnsForAnyArgs(x => new Person());
        personRepository.Create(default).ReturnsForAnyArgs(x => (Person)x[0]);

        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException("Business", (uint)x[0]));

        CreateInvoiceCommandHandler handler = new(invoiceRepository, personRepository, businessRepository);
        CreateInvoiceCommand command = new("testnumber", 0, 0, InvoiceStatusEnum.New, new List<InvoiceLineDto>(), 5, new PersonDto(1, null, null, null, null));

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }

    [Fact]
    public async Task PersonNotFoundShouldUseCreatePerson()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.Create(default).ReturnsForAnyArgs(x => (Invoice)x[0]);

        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException("Person", (uint)x[0]));
        personRepository.Create(default).ReturnsForAnyArgs(x => (Person)x[0]);

        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new Business());

        CreateInvoiceCommandHandler handler = new(invoiceRepository, personRepository, businessRepository);
        CreateInvoiceCommand command = new("testnumber", 0, 0, InvoiceStatusEnum.New, new List<InvoiceLineDto>(), 5, new PersonDto(1, null, null, null, null));

        var response = await handler.Handle(command, default);

        personRepository.Received().Create(Arg.Any<Person>());
        response.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task PersonFoundShouldNotUseCreatePerson()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.Create(default).ReturnsForAnyArgs(x => (Invoice)x[0]);

        IPersonRepository personRepository = Substitute.For<IPersonRepository>();
        personRepository.GetById(default).ReturnsForAnyArgs(x => new Person());
        personRepository.Create(default).ReturnsForAnyArgs(x => (Person)x[0]);

        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new Business());

        CreateInvoiceCommandHandler handler = new(invoiceRepository, personRepository, businessRepository);
        CreateInvoiceCommand command = new("testnumber", 0, 0, InvoiceStatusEnum.New, new List<InvoiceLineDto>(), 5, new PersonDto(1, null, null, null, null));

        var response = await handler.Handle(command, default);

        personRepository.DidNotReceive().Create(Arg.Any<Person>());
        response.IsSuccess.Should().BeTrue();
    }
}
