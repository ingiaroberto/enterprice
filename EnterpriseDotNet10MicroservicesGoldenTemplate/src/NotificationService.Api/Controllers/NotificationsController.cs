using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Features;

namespace NotificationService.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public sealed class NotificationsController : ControllerBase
{
    private readonly ISender _sender;

    public NotificationsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateNotificationResult), StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateNotificationResult>> Create(
        CreateNotificationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}
