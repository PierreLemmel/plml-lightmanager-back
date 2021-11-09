using LightManager.Infrastructure.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Infrastructure.CQRS.Commands
{
    public class CommandResult
    {
        public bool Success { get; }
        public string? ErrorMessage { get; }
        public IEnumerable<Event> Events { get; }

        private CommandResult(bool success, string? errorMessage, IEnumerable<Event> events)
        {
            Success = success;
            ErrorMessage = errorMessage;
            Events = events;
        }

        public static CommandResult Ok(params Event[] events) => new(true, null, events);
        public static CommandResult Error(string message) => new(false, message, Enumerable.Empty<Event>());
    }
}