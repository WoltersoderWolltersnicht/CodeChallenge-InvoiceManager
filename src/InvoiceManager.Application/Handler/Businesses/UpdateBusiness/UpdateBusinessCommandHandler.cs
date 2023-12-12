using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.UpdateBusiness;

public class UpdateBusinessQueryHandler : IRequestHandler<UpdateBusinessCommand, UpdateBusinessCommandResponse>
{
    public Task<UpdateBusinessCommandResponse> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
