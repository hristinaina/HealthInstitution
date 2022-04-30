using System;

namespace HealthInstitution.Exceptions
{
    class ExaminationRequestedException : Exception
    {
        public ExaminationRequestedException()
        {
        }

        public ExaminationRequestedException(string message)
            : base(message)
        {
        }

    }
}
