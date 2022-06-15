using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Exceptions
{
    class ZeroRoomNumberException : Exception
    {
        public ZeroRoomNumberException()
        {
        }

        public ZeroRoomNumberException(string message)
            : base(message)
        {
        }
    }
}
