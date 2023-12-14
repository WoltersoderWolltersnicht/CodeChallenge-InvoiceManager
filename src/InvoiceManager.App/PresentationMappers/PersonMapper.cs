using InvoiceManager.App.PresentationDto.People;
using InvoiceManager.Domain.People;

namespace InvoiceManager.App.PresentationMappers;

public static class PersonMapper
{
    public static PersonRs Map(Person person)
    {
        return new PersonRs()
        {
            Id = person.Id,
            NIF = person.NIF,
            Name = person.Name,
            Surname1 = person.Surname1,
            Surname2 = person.Surname2,
            Invoices = person.Invoices.Select(InvoiceMapper.Map).ToList(),
        };
    }
}
