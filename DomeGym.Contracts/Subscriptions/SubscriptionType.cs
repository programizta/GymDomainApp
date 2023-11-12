using System.Text.Json.Serialization;

namespace DomeGym.Contracts.Subscriptions;

// in order to convert it from Enum to String and the other way around
// while dealing with request, we need to mark it as a Json convertable enum
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubscriptionType
{
    Free,
    Premium
}