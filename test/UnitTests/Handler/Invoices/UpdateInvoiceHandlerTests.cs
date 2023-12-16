using FluentAssertions;
using InvoiceManager.Application.Handler.Invoices.UpdateInvoice;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using NSubstitute;

namespace UnitTests.Handler.Invoices;

public class UpdateInvoiceHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.Filter(default).ReturnsForAnyArgs(x => new List<Invoice>());
        invoiceRepository.GetById(default).ReturnsForAnyArgs(x => new Invoice()
        {
            Id = (uint)x[0],
            Estado = InvoiceStatusEnum.New
        });
        invoiceRepository.Update(default).ReturnsForAnyArgs(x => (Invoice)x[0]);

        UpdateInvoiceCommandHandler handler = new UpdateInvoiceCommandHandler(invoiceRepository);
        UpdateInvoiceCommand command = new(1, InvoiceStatusEnum.Anulada);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Invoice.Id.Should().Be(1);
        response.Value.Invoice.Estado.Should().Be(InvoiceStatusEnum.Anulada);
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IInvoiceRepository invoiceRepository = Substitute.For<IInvoiceRepository>();
        invoiceRepository.Filter(default).ReturnsForAnyArgs(x => new List<Invoice>());
        invoiceRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));
        invoiceRepository.Update(default).ReturnsForAnyArgs(x => (Invoice)x[0]);

        UpdateInvoiceCommandHandler handler = new UpdateInvoiceCommandHandler(invoiceRepository);
        UpdateInvoiceCommand command = new(1, InvoiceStatusEnum.Anulada);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
