using MediatR;

namespace InvoiceManager.Application.Handler.Businesses.DeleteBusiness;

public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand, DeleteBusinessCommandResponse>
{
    public Task<DeleteBusinessCommandResponse> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
