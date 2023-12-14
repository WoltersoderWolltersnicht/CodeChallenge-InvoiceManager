using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.People;
using MediatR;

namespace InvoiceManager.Application.Handler.People.UpdatePerson;

public record UpdatePersonCommand(Person Person) : IRequest<Result<UpdatePersonCommandResponse>>;
