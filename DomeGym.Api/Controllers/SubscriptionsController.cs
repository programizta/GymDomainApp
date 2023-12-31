using DomeGym.Application.Subscription.Commands.CreateSubscription;
using DomeGym.Application.Subscription.Commands.DeleteSubscription;
using DomeGym.Application.Subscription.Queries.GetSubscription;
using DomeGym.Contracts.Common;
using DomeGym.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;

[Route("[controller]")]
public class SubscriptionsController : ApiController
{
    private readonly IMediator _mediator; // or use ISender, because we implement only Send() method, so we could use smaller unit of interface => interface segregation principle

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(SubscriptionRequest request)
    {
        // TODO: implement discrete-enum type of SubscriptionType instead of passing a string
        // to command, as we should know up-front what subscription types are valid for further
        // processing
        var command = new CreateSubscriptionCommand(
            request.SubscriptionType.ToString(),
            request.AdminId);
        var createSubscriptionResult = await _mediator.Send(command);

        if (createSubscriptionResult.IsError)
        {
            return Problem();
        }

        var response = new SubscriptionResponse(createSubscriptionResult.Value.Id, request.SubscriptionType);

        return Ok(response);
    }

    [HttpDelete("{subscriptionId:guid}")]
    public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
    {
        var command = new DeleteSubscriptionCommand(subscriptionId);
        var deleteSubscriptionResult = await _mediator.Send(command);

        if (deleteSubscriptionResult.IsError)
        {
            return Problem(string.Format(deleteSubscriptionResult.FirstError.Description, subscriptionId.ToString()));
        }

        var response = new MessageResponse(string.Format("Subscription with ID: {0} was deleted", subscriptionId.ToString()));

        return Ok(response);
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);
        var getSubscriptionResult = await _mediator.Send(query);

        if (getSubscriptionResult.IsError)
        {
            return Problem(getSubscriptionResult.Errors);
        }
        
        var response = new SubscriptionResponse(
            subscriptionId,
            Enum.Parse<SubscriptionType>(getSubscriptionResult.Value.SubscriptionDetails.SubscriptionName));

        return Ok(response);
    }
}