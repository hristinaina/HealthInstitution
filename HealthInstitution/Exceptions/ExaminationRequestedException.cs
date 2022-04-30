using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
