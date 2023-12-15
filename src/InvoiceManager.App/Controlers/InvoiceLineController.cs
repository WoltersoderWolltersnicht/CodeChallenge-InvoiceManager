using InvoiceManager.App.PresentationDto.InvoiceLines;
using InvoiceManager.App.PresentationMappers;
using InvoiceManager.Application.Handler.InvoiceLines.CreateInvoiceLine;
using InvoiceManager.Application.Handler.InvoiceLines.DeleteInvoiceLine;
using InvoiceManager.Application.Handler.InvoiceLines.GetInvoiceLine;
using InvoiceManager.Application.Handler.InvoiceLines.UpdateInvoiceLine;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManager.App.Controlers;

[ApiController]
[Route("[controller]")]
public class InvoiceLineController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoiceLineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(uint id)
    {
        var queryResult = await _mediator.Send(new GetInvoiceLineQuery(id));
        if (!queryResult.IsSuccess) return AdviseMessageMapper.Map(queryResult.Error);
        return Ok(InvoiceLineMapper.Map(queryResult.Value.InvoiceLine));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateInvoiceLineCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        if (!commandResult.IsSuccess) return AdviseMessageMapper.Map(commandResult.Error);
        return Ok(InvoiceLineMapper.Map(commandResult.Value.InvoiceLine));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var commandResult = await _mediator.Send(new DeleteInvoiceLineCommand(id));
        if (!commandResult.IsSuccess) return AdviseMessageMapper.Map(commandResult.Error);
        return Ok(InvoiceLineMapper.Map(commandResult.Value.InvoiceLine));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateInvoiceLineCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        if (!commandResult.IsSuccess) return AdviseMessageMapper.Map(commandResult.Error);
        return Ok(InvoiceLineMapper.Map(commandResult.Value.InvoiceLine));
    }
}
