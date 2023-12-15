using FluentAssertions;
using InvoiceManager.Application.Handler.Businesses.CreateBusiness;
using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Exceptions;
using NSubstitute;

namespace UnitTests.Handler.Businesses;

public class CreateBusinessHandlerTests
{
    [Fact]
    public async Task Ok()
    {
        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.Filter(default).ReturnsForAnyArgs(new List<Business>());
        businessRepository.Create(default).ReturnsForAnyArgs(x => (Business)x[0]);

        CreateBusinessCommandHandler handler = new CreateBusinessCommandHandler(businessRepository);
        CreateBusinessCommand command = new("TestName", "TestCIF");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Business.CIF.Should().Be(command.CIF);
        response.Value.Business.Name.Should().Be(command.Name);
    }

    [Fact]
    public async Task HandleDuplicateCIFOrName()
    {
        IBusinessRepository businessRepository = Substitute.For<IBusinessRepository>();
        businessRepository.Filter(default).ReturnsForAnyArgs(new List<Business>() { new Business() });
        businessRepository.Create(default).ReturnsForAnyArgs(x => (Business)x[0]);

        CreateBusinessCommandHandler handler = new CreateBusinessCommandHandler(businessRepository);
        CreateBusinessCommand command = new("TestName", "TestCIF");

        var response = await handler.Handle(command, default);

        response.IsSuccess.Should().BeFalse();
        response.Error.Should().BeOfType(typeof(DupplicateKeyException));
    }
}
