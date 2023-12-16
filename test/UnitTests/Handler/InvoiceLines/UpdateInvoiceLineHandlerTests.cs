using FluentAssertions;
using InvoiceManager.Application.Handler.InvoiceLines.UpdateInvoiceLine;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using NSubstitute;

namespace UnitTests.Handler.InvoiceLines;

public class UpdateInvoiceLineHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IInvoiceLineRepository invoiceLineRepository = Substitute.For<IInvoiceLineRepository>();
        invoiceLineRepository.Filter(default).ReturnsForAnyArgs(x => new List<InvoiceLine>());
        invoiceLineRepository.GetById(default).ReturnsForAnyArgs(x => new InvoiceLine()
        {
            Id = (uint)x[0],
            Amount = 0,
            VAT = 0,
            Invoice = new Invoice()
            {
                Id = 1,
                Amount = 50,
            }
        });
        invoiceLineRepository.Update(default).ReturnsForAnyArgs(x => (InvoiceLine)x[0]);

        UpdateInvoiceLineCommandHandler handler = new(invoiceLineRepository);
        UpdateInvoiceLineCommand command = new(1, 50, 50);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.InvoiceLine.Id.Should().Be(1);
        response.Value.InvoiceLine.Amount.Should().Be(50);
        response.Value.InvoiceLine.VAT.Should().Be(50);
        response.Value.InvoiceLine.Invoice.Amount.Should().Be(100);
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IInvoiceLineRepository invoiceLineRepository = Substitute.For<IInvoiceLineRepository>();
        invoiceLineRepository.Filter(default).ReturnsForAnyArgs(x => new List<InvoiceLine>());
        invoiceLineRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));
        invoiceLineRepository.Update(default).ReturnsForAnyArgs(x => (InvoiceLine)x[0]);

        UpdateInvoiceLineCommandHandler handler = new(invoiceLineRepository);
        UpdateInvoiceLineCommand command = new(1, 50, 50);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}