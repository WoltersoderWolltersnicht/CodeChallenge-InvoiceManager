using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.People.DeletePerson;

public record DeletePersonCommand(uint Id) : IRequest<Result<DeletePersonCommandResponse>>;