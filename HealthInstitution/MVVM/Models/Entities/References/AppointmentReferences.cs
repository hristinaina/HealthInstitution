using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class AppointmentReferences
    {
        private int _appoinmentId;
        private int _doctorId;
        private int _patientId;
        private int _roomId;
        private int _perscriptionId;

        public AppointmentReferences(int appointmentId, int doctoriId, int patientId,
                                     int roomId, int perscriptionId)
        {
            _appoinmentId = appointmentId;
            _doctorId = doctoriId;
            _patientId = patientId;
            _roomId = roomId;
            _perscriptionId = perscriptionId;
        }
    }
}
