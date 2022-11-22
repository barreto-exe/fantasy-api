using System;

namespace FantasyApi.Data.Users.Exceptions
{
    public class UserDoesntExistException : Exception
    {
        public UserDoesntExistException() : base("User with that id doesn't exist.") { }
    }
}
