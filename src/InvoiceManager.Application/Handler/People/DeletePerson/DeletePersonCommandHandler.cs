using MediatR;

namespace InvoiceManager.Application.Handler.People.DeletePerson;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, DeletePersonCommandResponse>
{
    public Task<DeletePersonCommandResponse> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
