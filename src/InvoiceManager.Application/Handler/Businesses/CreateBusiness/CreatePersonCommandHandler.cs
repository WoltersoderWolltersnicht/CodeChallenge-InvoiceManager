using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.CreateBusiness;

public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, CreateBusinessCommandResponse>
{
    public Task<CreateBusinessCommandResponse> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
