using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.People.GetPerson;

public record GetPersonQuery(uint Id) : IRequest<Result<GetPersonQueryResponse>>;
