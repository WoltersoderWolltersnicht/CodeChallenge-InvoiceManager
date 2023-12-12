using MediatR;

namespace InvoiceManager.Application.Handler.People.UpdatePerson;

public class GetPersonQueryHandler : IRequestHandler<UpdateBusinessCommand, UpdatePersonCommandResponse>
{
    public Task<UpdatePersonCommandResponse> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
