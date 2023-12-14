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
    public async Task<ActionResult<InvoiceLineRs>> Get(uint id)
    {
        var queryResult = await _mediator.Send(new GetInvoiceLineQuery(id));
        return InvoiceLineMapper.Map(queryResult.Value.InvoiceLine);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceLineRs>> Post([FromBody] CreateInvoiceLineCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        return InvoiceLineMapper.Map(commandResult.Value.InvoiceLine);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<InvoiceLineRs>> Delete(uint id)
    {
        var commandResult = await _mediator.Send(new DeleteInvoiceLineCommand(id));
        return InvoiceLineMapper.Map(commandResult.Value.InvoiceLine);
    }

    [HttpPut]
    public async Task<ActionResult<InvoiceLineRs>> Put([FromBody] UpdateInvoiceLineCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        return InvoiceLineMapper.Map(commandResult.Value.InvoiceLine);
    }
}
