namespace LightManager.Infrastructure.CQRS.Contracts
{
    public interface IData
    {
        object Data { get; }
    }

    public interface IData<TData> : IData
    {
        new TData Data { get; }
    }
}
