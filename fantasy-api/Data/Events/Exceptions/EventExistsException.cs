using System;

namespace FantasyApi.Data.Events.Exceptions
{
    public class EventExistsException : Exception
    {
        public EventExistsException() : base("Name has already been used.") { }
    }
}
