using System;

namespace FantasyApi.Data.Base.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string description) : base($"{description} was not found.") { }
    }
}
