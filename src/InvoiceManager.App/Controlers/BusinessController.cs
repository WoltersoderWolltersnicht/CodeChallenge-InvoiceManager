using InvoiceManager.App.PresentationMappers;
using InvoiceManager.Application.Handler.Businesses.CreateBusiness;
using InvoiceManager.Application.Handler.Businesses.DeleteBusiness;
using InvoiceManager.Application.Handler.Businesses.GetBusiness;
using InvoiceManager.Application.Handler.Businesses.UpdateBusiness;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManager.App.Controlers;

[ApiController]
[Route("[controller]")]
public class BusinessController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(uint id)
    {
        var queryResult = await _mediator.Send(new GetBusinessQuery(id));
        if (!queryResult.IsSuccess) return AdviseMessageMapper.Map(queryResult.Error);
        return Ok(BusinessMapper.Map(queryResult.Value.Business));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBusinessCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        if (!commandResult.IsSuccess) return AdviseMessageMapper.Map(commandResult.Error);
        return Ok(BusinessMapper.Map(commandResult.Value.Business));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var commandResult = await _mediator.Send(new DeleteBusinessCommand(id));
        if (!commandResult.IsSuccess) return AdviseMessageMapper.Map(commandResult.Error);
        return Ok(BusinessMapper.Map(commandResult.Value.Business));
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateBusinessCommand createCommand)
    {
        var commandResult = await _mediator.Send(createCommand);
        if (!commandResult.IsSuccess) return AdviseMessageMapper.Map(commandResult.Error);
        return Ok(BusinessMapper.Map(commandResult.Value.Business));
    }
}
