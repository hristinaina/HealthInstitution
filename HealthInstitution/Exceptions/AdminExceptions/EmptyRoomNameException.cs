using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions.AdminExceptions
{
    class EmptyRoomNameException : Exception
    {
        public EmptyRoomNameException()
        {
        }

        public EmptyRoomNameException(string message)
            : base(message)
        {
        }
    }
}
