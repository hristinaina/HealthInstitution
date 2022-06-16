using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Exceptions
{
    class RoomNumberAlreadyTakenException : Exception
    {
        public RoomNumberAlreadyTakenException() 
        {
        }

        public RoomNumberAlreadyTakenException(string message)
        : base(message)
            {
            }
    }
}
