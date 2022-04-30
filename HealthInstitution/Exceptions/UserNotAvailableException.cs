using System;

namespace HealthInstitution.Exceptions
{
    class UserNotAvailableException : Exception
    {
        public UserNotAvailableException()
        {
        }

        public UserNotAvailableException(string message)
            : base(message)
        {
        }
    }
}
