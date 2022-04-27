using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    class OperationReferences
    {
        private int _appoinmentId;
        private int _doctorId;
        private int _patientId;
        private int _roomId;

        public OperationReferences(int appointmentId, int doctoriId, int patientId, int roomId)
        {
            _appoinmentId = appointmentId;
            _doctorId = doctoriId;
            _patientId = patientId;
            _roomId = roomId;
        }
    }
}
