using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.People.UpdatePerson;

public record UpdatePersonCommand(uint Id, string? Name, string? Surname1, string? Surname2, string? NIF) : IRequest<Result<UpdatePersonCommandResponse>>;
