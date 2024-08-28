namespace Team_Plus_Automotive_Telemetry_API.Handlers
{
    public interface IHandler<TRequest, TResponse>
    {
        TResponse Handle(TRequest request);
    }
}
