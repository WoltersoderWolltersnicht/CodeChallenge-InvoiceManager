using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.Common;
using InvoiceManager.Domain.Exceptions;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using InvoiceManager.Domain.People;
using MediatR;

namespace InvoiceManager.Application.Handler.Invoices.CreateInvoice;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<CreateInvoiceCommandResponse>>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceLineRepository _invoiceLineRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IBusinessRepository _businessRepository;

    public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IInvoiceLineRepository invoiceLineRepository, IPersonRepository personRepository, IBusinessRepository businessRepository)
    {
        _invoiceRepository = invoiceRepository;
        _invoiceLineRepository = invoiceLineRepository;
        _personRepository = personRepository;
        _businessRepository = businessRepository;
    }

    public async Task<Result<CreateInvoiceCommandResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var getBusinessResponse = await _businessRepository.GetById(request.BusinessId);
        if (!getBusinessResponse.IsSuccess) return getBusinessResponse.Error;

        var personResponse = await ValidatePersonAndCreateIfNotExsits(request.Person);
        if (!personResponse.IsSuccess) return personResponse.Error;

        var createInvoiceResponse = await _invoiceRepository.Create(new Invoice()
        {
            GUID = Guid.NewGuid().ToString(),
            Estado = request.Status,
            Business = getBusinessResponse.Value,
            Number = request.Number,
            Person = personResponse.Value,
            VAT = request.VAT,
            Amount = request.InvoiceLines.Sum(il => il.InvoiceLineAmount),
            InvoiceLines = request.InvoiceLines.Select(il => new InvoiceLine()
            {
                Amount = il.InvoiceLineAmount,
                VAT = il.InvoiceLineVAT
            }).ToList()
        });

        if (!createInvoiceResponse.IsSuccess) return createInvoiceResponse.Error;
        return new CreateInvoiceCommandResponse(createInvoiceResponse.Value);
    }

    private async Task<Result<Person>> ValidatePersonAndCreateIfNotExsits(PersonDto person)
    {
        if (person.Id != null)
        {
            var getPersonResponse = await _personRepository.GetById(person.Id.Value);
            if (getPersonResponse.IsSuccess) return getPersonResponse;
        }

        var createPersonResult = await _personRepository.Create(new Person()
        {
            Name = person.Name,
            Surname1 = person.Surname1,
            Surname2 = person.Surname2,
            Invoices = new(),
            NIF = person.NIF
        });

        if (!createPersonResult.IsSuccess) return new DatabaseException("Person could not be saved correctly");
        return createPersonResult;
    }
}
