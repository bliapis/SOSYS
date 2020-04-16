namespace LT.SO.Domain.Core.Events
{
    public interface IRejectedEvent
    {
        string Code { get; }
        string Reason { get; }
    }
}