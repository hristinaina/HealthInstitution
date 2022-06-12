using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Exceptions
{
    class RoomUnderRenovationException : Exception
    {
        public RoomUnderRenovationException()
        {
        }

        public RoomUnderRenovationException(string message)
            : base(message)
        {
        }
    }
}
