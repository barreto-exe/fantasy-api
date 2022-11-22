using System;

namespace FantasyApi.Data.Users.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException() : base("Email has already been used.") { }
    }
}
