using System;

namespace HealthInstitution.Core.Exceptions
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
