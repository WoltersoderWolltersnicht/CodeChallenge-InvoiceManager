using FluentAssertions;
using InvoiceManager.Application.Handler.Invoices.GetInvoice;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using NSubstitute;

namespace UnitTests.Handler.Invoices;

public class GetInvoiceHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.GetById(default).ReturnsForAnyArgs(x => new Invoice()
        {
            Id = (uint)x[0],
            Amount = 50
        });

        GetInvoiceQueryHandler handler = new(invoiceRepository);
        GetInvoiceQuery command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Invoice.Id.Should().Be(1);
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));

        GetInvoiceQueryHandler handler = new(invoiceRepository);
        GetInvoiceQuery command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
