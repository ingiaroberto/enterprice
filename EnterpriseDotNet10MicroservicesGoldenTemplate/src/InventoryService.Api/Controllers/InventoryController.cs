using MediatR;
using Microsoft.AspNetCore.Mvc;
using InventoryService.Application.Features;

namespace InventoryService.Api.Controllers;

[ApiController]
[Route("api/inventory")]
public sealed class InventoryController : ControllerBase
{
    private readonly ISender _sender;

    public InventoryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateInventoryResult), StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateInventoryResult>> Create(
        CreateInventoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}
