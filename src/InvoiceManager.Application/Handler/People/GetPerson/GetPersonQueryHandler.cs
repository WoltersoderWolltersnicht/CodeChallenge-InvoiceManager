using MediatR;

namespace InvoiceManager.Application.Handler.People.GetPerson;

public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, GetPersonQueryResponse>
{
    public Task<GetPersonQueryResponse> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
