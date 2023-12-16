using InvoiceManager.App.PresentationMappers;
using InvoiceManager.Application.Handler.Invoices.CreateInvoice;
using InvoiceManager.Application.Handler.Invoices.DeleteInvoice;
using InvoiceManager.Application.Handler.Invoices.FilterInvoice;
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
    public async Task<IActionResult> Get(uint id)
    {
        var queryResult = await _mediator.Send(new GetInvoiceQuery(id));
        if (!queryResult.IsSuccess) return AdviseMessageMapper.Map(queryResult.Error);
        return Ok(InvoiceMapper.Map(queryResult.Value.Invoice));
    }

    [HttpPost("Filter")]
    public async Task<IActionResult> Filter([FromBody] FilterInvoiceQuery filterQuery)
    {
        var queryResult = await _mediator.Send(filterQuery);
        if (!queryResult.IsSuccess) return AdviseMessageMapper.Map(queryResult.Error);
        var invoicesDto = queryResult.Value.Invoices.Select(InvoiceMapper.Map);
        return Ok(invoicesDto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateInvoiceCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        if (!commandResult.IsSuccess) return AdviseMessageMapper.Map(commandResult.Error);
        return Ok(InvoiceMapper.Map(commandResult.Value.Invoice));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var commandResult = await _mediator.Send(new DeleteInvoiceCommand(id));
        if (!commandResult.IsSuccess) return AdviseMessageMapper.Map(commandResult.Error);
        return Ok(InvoiceMapper.Map(commandResult.Value.Invoice));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateInvoiceCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        if (!commandResult.IsSuccess) return AdviseMessageMapper.Map(commandResult.Error);
        return Ok(InvoiceMapper.Map(commandResult.Value.Invoice));
    }
}
