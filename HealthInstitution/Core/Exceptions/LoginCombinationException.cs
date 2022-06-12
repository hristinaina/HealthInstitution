using System;

namespace HealthInstitution.Core.Exceptions
{
    class LoginCombinationException : Exception
    {
        public LoginCombinationException()
        {
        }

        public LoginCombinationException(string message)
            : base(message)
        {
        }

    }
}
