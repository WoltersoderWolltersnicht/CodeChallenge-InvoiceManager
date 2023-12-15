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
    public async Task<IActionResult> Get(uint id)
    {
        var queryResponse = await _mediator.Send(new GetPersonQuery(id));
        if (!queryResponse.IsSuccess) return AdviseMessageMapper.Map(queryResponse.Error);
        return Ok(PersonMapper.Map(queryResponse.Value.Person));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePersonCommand createCommand)
    {
        var commandResponse = await _mediator.Send(createCommand);
        if (!commandResponse.IsSuccess) return AdviseMessageMapper.Map(commandResponse.Error);
        return Ok(PersonMapper.Map(commandResponse.Value.Person));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var commandResponse = await _mediator.Send(new DeletePersonCommand(id));
        if (!commandResponse.IsSuccess) return AdviseMessageMapper.Map(commandResponse.Error);
        return Ok(PersonMapper.Map(commandResponse.Value.Person));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdatePersonCommand createCommand)
    {
        var commandResponse = await _mediator.Send(createCommand);
        if (!commandResponse.IsSuccess) return AdviseMessageMapper.Map(commandResponse.Error);
        return Ok(PersonMapper.Map(commandResponse.Value.Person));
    }
}
