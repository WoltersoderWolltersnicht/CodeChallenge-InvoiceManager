using InvoiceManager.Domain.People;
using MediatR;

namespace InvoiceManager.Application.Handler.People.CreatePerson;

public record CreatePersonCommand(Person Person) : IRequest<CreatePersonCommandResponse>;