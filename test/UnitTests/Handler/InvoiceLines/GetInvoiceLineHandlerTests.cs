using FluentAssertions;
using InvoiceManager.Application.Handler.InvoiceLines.GetInvoiceLine;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using NSubstitute;

namespace UnitTests.Handler.InvoiceLines;

public class GetInvoiceLineHandlerTests
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

        GetInvoiceLineQueryHandler handler = new(invoiceLineRepository);
        GetInvoiceLineQuery command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.InvoiceLine.Id.Should().Be(1);
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IInvoiceLineRepository invoiceLineRepository = Substitute.For<IInvoiceLineRepository>();
        invoiceLineRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));

        GetInvoiceLineQueryHandler handler = new(invoiceLineRepository);
        GetInvoiceLineQuery command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
