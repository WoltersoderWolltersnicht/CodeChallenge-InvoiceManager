using FluentAssertions;
using InvoiceManager.Application.Handler.Businesses.DeleteBusiness;
using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using NSubstitute;

namespace UnitTests.Handler.Businesses;

public class DeleteBusinessHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new Business() 
        { 
            Id = (uint)x[0],
            Name = "testname",
            CIF = "testcif",
            Invoices = new List<Invoice>()
        });

        businessRepository.Delete(default).ReturnsForAnyArgs(x => (Business)x[0]);

        DeleteBusinessCommandHandler handler = new DeleteBusinessCommandHandler(businessRepository);
        DeleteBusinessCommand command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Business.Id.Should().Be(1);
        response.Value.Business.CIF.Should().Be("testcif");
        response.Value.Business.Name.Should().Be("testname");
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));
        businessRepository.Delete(default).ReturnsForAnyArgs(x => (Business)x[0]);

        DeleteBusinessCommandHandler handler = new DeleteBusinessCommandHandler(businessRepository);
        DeleteBusinessCommand command = new(1);

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
