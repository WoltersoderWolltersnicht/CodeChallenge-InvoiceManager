using InvoiceManager.Domain.Common;
using MediatR;

namespace InvoiceManager.Application.Handler.People.CreatePerson;

public record CreatePersonCommand(string NIF, string Name, string Surname1, string Surname2) : IRequest<Result<CreatePersonCommandResponse>>;