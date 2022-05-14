using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions.AdminExceptions
{
    class RoomCannotBeChangedException : Exception
    {
        public RoomCannotBeChangedException()
        {
        }

        public RoomCannotBeChangedException(string message) : base(message)
        {
        }
    }
}
