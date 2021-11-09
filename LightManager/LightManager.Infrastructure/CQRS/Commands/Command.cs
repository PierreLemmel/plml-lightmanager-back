using LightManager.Infrastructure.CQRS.Contracts;
using System;

namespace LightManager.Infrastructure.CQRS.Commands
{
    public abstract class Command : IData
    {
        public DateTime Time { get; }
        public string CommandType => GetType().Name;

        protected virtual object GetData() => new object();
        public object Data => GetData();

        protected Command(DateTime time)
        {
            Time = time;
        }
    }

    public abstract class Command<TData> : Command
        where TData : notnull
    {
        public new TData Data { get; }

        protected override sealed object GetData() => Data;

        protected Command(DateTime time, TData data) : base(time)
        {
            Data = data;
        }
    }
}