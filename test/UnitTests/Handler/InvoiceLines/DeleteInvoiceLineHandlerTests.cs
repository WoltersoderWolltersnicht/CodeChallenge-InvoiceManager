using FluentAssertions;
using InvoiceManager.Application.Handler.InvoiceLines.DeleteInvoiceLine;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using NSubstitute;

namespace UnitTests.Handler.InvoiceLines;

public class DeleteInvoiceLineHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IInvoiceLineRepository invoiceLineRepository = Substitute.For<IInvoiceLineRepository>();
        invoiceLineRepository.GetById(default).ReturnsForAnyArgs(x => new InvoiceLine()
        {
            Id = (uint)x[0],
            Amount = 50,
            Invoice = new Invoice()
            {
                Id = 1,
                Amount = 50,
            }
        });
        invoiceLineRepository.Delete(default).ReturnsForAnyArgs(x => (InvoiceLine)x[0]);

        DeleteInvoiceLineCommandHandler handler = new(invoiceLineRepository);
        DeleteInvoiceLineCommand command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.InvoiceLine.Id.Should().Be(1);
        response.Value.InvoiceLine.Invoice.Should().NotBeNull();
        response.Value.InvoiceLine.Invoice.Amount.Should().Be(0);
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IInvoiceLineRepository invoiceLineRepository = Substitute.For<IInvoiceLineRepository>();
        invoiceLineRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));
        invoiceLineRepository.Delete(default).ReturnsForAnyArgs(x => (InvoiceLine)x[0]);

        DeleteInvoiceLineCommandHandler handler = new(invoiceLineRepository);
        DeleteInvoiceLineCommand command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
