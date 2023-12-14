using InvoiceManager.App.PresentationDto.Invoices;
using InvoiceManager.App.PresentationMappers;
using InvoiceManager.Application.Handler.Invoices.CreateInvoice;
using InvoiceManager.Application.Handler.Invoices.DeleteInvoice;
using InvoiceManager.Application.Handler.Invoices.GetInvoice;
using InvoiceManager.Application.Handler.Invoices.UpdateInvoice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManager.App.Controlers;

[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvoiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceRs>> Get(uint id)
    {
        var queryResult = await _mediator.Send(new GetInvoiceQuery(id));
        return InvoiceMapper.Map(queryResult.Value.Invoice);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceRs>> Post([FromBody] CreateInvoiceCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        return InvoiceMapper.Map(commandResult.Value.Invoice);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<InvoiceRs>> Delete(uint id)
    {
        var commandResult = await _mediator.Send(new DeleteInvoiceCommand(id));
        return InvoiceMapper.Map(commandResult.Value.Invoice);
    }

    [HttpPut]
    public async Task<ActionResult<InvoiceRs>> Put([FromBody] UpdateInvoiceCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        return InvoiceMapper.Map(commandResult.Value.Invoice);
    }
}
