using System;

namespace HealthInstitution.Exceptions
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
