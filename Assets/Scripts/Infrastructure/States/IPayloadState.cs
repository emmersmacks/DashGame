namespace Infrastructure.States
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        void Enter<TPayload>(TPayload payload);
    }
}