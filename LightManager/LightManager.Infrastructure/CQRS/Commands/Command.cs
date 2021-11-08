using System;

namespace LightManager.Infrastructure.CQRS.Commands
{
    public abstract class Command
    {
        public DateTime Time { get; }
        public string CommandType => GetType().Name;

        protected Command(DateTime time)
        {
            Time = time;
        }
    }

    public abstract class Command<TData> : Command
    {
        public TData Data { get; }

        protected Command(DateTime time, TData data) : base(time)
        {
            Data = data;
        }
    }
}