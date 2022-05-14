using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Exceptions.AdminExceptions
{
    class RearrangeTargetRoomNullException : Exception
    {
        public RearrangeTargetRoomNullException()
        {
        }

        public RearrangeTargetRoomNullException(string message)
            : base(message)
        {
        }
    }
}
