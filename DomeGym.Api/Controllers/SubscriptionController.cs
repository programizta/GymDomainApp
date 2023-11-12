using DomeGym.Application.Subscription.Commands.CreateSubscription;
using DomeGym.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly IMediator _mediator; // or use ISender, because we implement only Send() method, so we could use smaller unit of interface => interface segregation principle

    public SubscriptionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        var command = new CreateSubscriptionCommand(
            request.SubscriptionType.ToString(),
            request.AdminId);
        var subscriptionId = await _mediator.Send(command);
        var response = new SubscriptionResponse(subscriptionId, request.SubscriptionType);

        return Ok(response);
    }
}