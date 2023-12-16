using FluentAssertions;
using InvoiceManager.Application.Handler.Invoices.DeleteInvoice;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using NSubstitute;

namespace UnitTests.Handler.Invoices;

public class DeleteInvoiceHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.GetById(default).ReturnsForAnyArgs(x => new Invoice()
        {
            Id = 1,
            Amount = 50,
        });
        invoiceRepository.Delete(default).ReturnsForAnyArgs(x => (Invoice)x[0]);

        DeleteInvoiceCommandHandler handler = new(invoiceRepository);
        DeleteInvoiceCommand command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Invoice.Id.Should().Be(1);
        response.Value.Invoice.Amount.Should().Be(50);
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));
        invoiceRepository.Delete(default).ReturnsForAnyArgs(x => (Invoice)x[0]);

        DeleteInvoiceCommandHandler handler = new(invoiceRepository);
        DeleteInvoiceCommand command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
