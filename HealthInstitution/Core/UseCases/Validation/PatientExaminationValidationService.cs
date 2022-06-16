using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core.Services.DoctorServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services.ValidationServices
{
    class PatientExaminationValidationService
    {
        private Appointment _appointment;
        private DateTime _dateTime;

        public PatientExaminationValidationService(Appointment appointment, DateTime dateTime)
        {
            _appointment = appointment;
            _dateTime = dateTime;
        }


        public void ValidateAppointmentData()
        {
            {
                if (_appointment.Doctor is null)
                {
                    throw new EmptyFieldException("Doctor not selected !");
                }
                ExaminationService appointmentService = new ExaminationService();
                int duration = appointmentService.GetDuration(_appointment);
                PatientService patientService = new PatientService(_appointment.Patient);
                DoctorService doctorService = new DoctorService(_appointment.Doctor);
                if (DateTime.Compare(DateTime.Now, _dateTime) > 0)
                {
                    throw new DateException("Date must be in future !");
                }
                if ((_dateTime - DateTime.Now).TotalDays < 1)
                {
                    throw new DateException("Cannot schedule in next 24 hours");
                }
                if (!patientService.IsAvailable(_dateTime, duration))
                {
                    throw new UserNotAvailableException("Patient not available at selected time !");
                }
                if (!doctorService.IsAvailable(_dateTime, duration))
                {
                    throw new UserNotAvailableException("Doctor not available at selected time !");
                }
            }
        }
    }
}
