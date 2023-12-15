using FluentAssertions;
using InvoiceManager.Application.Handler.InvoiceLines.CreateInvoiceLine;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using NSubstitute;

namespace UnitTests.Handler.Businesses;

public class CreateInvoiceLineHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IInvoiceLineRepository invoiceLineRepository = Substitute.For<IInvoiceLineRepository>();
        invoiceLineRepository.Create(default).ReturnsForAnyArgs(x => (InvoiceLine)x[0]);

        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.GetById(default).ReturnsForAnyArgs(x => new Invoice()
        {
            Id = (uint)x[0],
            Amount = 0,
        });

        CreateInvoiceLineCommandHandler handler = new(invoiceLineRepository, invoiceRepository);
        CreateInvoiceLineCommand command = new(1, 50, 50);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.InvoiceLine.VAT.Should().Be(command.InvoiceLineVAT);
        response.Value.InvoiceLine.Amount.Should().Be(command.InvoiceLineAmount);
        response.Value.InvoiceLine.Invoice.Should().NotBeNull();
        response.Value.InvoiceLine.Invoice.Id.Should().Be(1);
        response.Value.InvoiceLine.Invoice.Amount.Should().Be(50);
    }

    [Fact]
    public async Task HandleInvoiceNotFound()
    {
        IInvoiceLineRepository invoiceLineRepository = Substitute.For<IInvoiceLineRepository>();
        invoiceLineRepository.Create(default).ReturnsForAnyArgs(x => (InvoiceLine)x[0]);

        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));

        CreateInvoiceLineCommandHandler handler = new(invoiceLineRepository, invoiceRepository);
        CreateInvoiceLineCommand command = new(1, 50, 50);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
