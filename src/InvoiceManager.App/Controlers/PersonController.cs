using InvoiceManager.App.PresentationDto.People;
using InvoiceManager.App.PresentationMappers;
using InvoiceManager.Application.Handler.People.CreatePerson;
using InvoiceManager.Application.Handler.People.DeletePerson;
using InvoiceManager.Application.Handler.People.GetPerson;
using InvoiceManager.Application.Handler.People.UpdatePerson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManager.App.Controlers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonRs>> Get(uint id)
    {
        var queryResponse = await _mediator.Send(new GetPersonQuery(id));
        return PersonMapper.Map(queryResponse.Value.Person);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePersonCommand createCommand)
    {
        var commandResponse = await _mediator.Send(createCommand);
        if (!commandResponse.IsSuccess) return AdviseMessageMapper.Map(commandResponse.Error);
        return Ok(PersonMapper.Map(commandResponse.Value.Person));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PersonRs>> Delete(uint id)
    {
        var commandResponse = await _mediator.Send(new DeletePersonCommand(id));
        return PersonMapper.Map(commandResponse.Value.Person);
    }

    [HttpPut]
    public async Task<ActionResult<PersonRs>> Put([FromBody] UpdatePersonCommand createCommand)
    {
        var commandResponse = await _mediator.Send(createCommand);
        return PersonMapper.Map(commandResponse.Value.Person);
    }
}
