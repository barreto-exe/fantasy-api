using System;

namespace FantasyApi.Data.Base.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string description) : base($"{description} already exists.") { }
    }
}
