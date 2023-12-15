using FluentAssertions;
using InvoiceManager.Application.Handler.Businesses.UpdateBusiness;
using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.Invoices;
using NSubstitute;

namespace UnitTests.Handler.Businesses;

public class UpdateBusinessHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.Filter(default).ReturnsForAnyArgs(x => new List<Business>());
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new Business()
        {
            Id = (uint)x[0],
            Name = "testname",
            CIF = "testcif",
            Invoices = new List<Invoice>()
        });
        businessRepository.Update(default).ReturnsForAnyArgs(x => (Business)x[0]);

        UpdateBusinessCommandHandler handler = new UpdateBusinessCommandHandler(businessRepository);
        UpdateBusinessCommand command = new(1, "updatedName", "updatedCIF");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Business.Id.Should().Be(1);
        response.Value.Business.CIF.Should().Be("updatedCIF");
        response.Value.Business.Name.Should().Be("updatedName");
    }

    [Fact]
    public async Task HandleDuplicateCIFOrName()
    {
        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.Filter(default).ReturnsForAnyArgs(x => new List<Business>() { new Business() });
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new Business()
        {
            Id = (uint)x[0],
            Name = "testname",
            CIF = "testcif",
            Invoices = new List<Invoice>()
        });
        businessRepository.Update(default).ReturnsForAnyArgs(x => (Business)x[0]);

        UpdateBusinessCommandHandler handler = new UpdateBusinessCommandHandler(businessRepository);
        UpdateBusinessCommand command = new(1, "updatedName", "updatedCIF");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(DupplicateKeyException));
    }

    [Fact]
    public async Task HandleNotFound()
    {
        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.Filter(default).ReturnsForAnyArgs(x => new List<Business>() { });
        businessRepository.GetById(default).ReturnsForAnyArgs(x => new IdNotFoundException((uint)x[0]));
        businessRepository.Update(default).ReturnsForAnyArgs(x => (Business)x[0]);

        UpdateBusinessCommandHandler handler = new UpdateBusinessCommandHandler(businessRepository);
        UpdateBusinessCommand command = new(1, "updatedName", "updatedCIF");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(IdNotFoundException));
    }
}
