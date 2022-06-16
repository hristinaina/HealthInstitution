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
        private ExaminationService _examinationService;
        private PatientService _patientService;
        private DoctorService _doctorService;

        public PatientExaminationValidationService()
        {
            _examinationService = new ExaminationService();
            _patientService = new PatientService();
        }

        public void ValidateAppointmentData(Appointment appointment, DateTime dateTime)
        {
            {
                int duration = _examinationService.GetDuration(appointment);

                if (appointment.Doctor is null)
                {
                    throw new EmptyFieldException("Doctor not selected !");
                }

                _doctorService = new DoctorService(appointment.Doctor);

                if (DateTime.Compare(DateTime.Now, dateTime) > 0)
                {
                    throw new DateException("Date must be in future !");
                }
                if ((dateTime - DateTime.Now).TotalDays < 1)
                {
                    throw new DateException("Cannot schedule in next 24 hours");
                }
                if (!_patientService.IsAvailable(dateTime, duration))
                {
                    throw new UserNotAvailableException("Patient not available at selected time !");
                }
                if (!_doctorService.IsAvailable(dateTime, duration))
                {
                    throw new UserNotAvailableException("Doctor not available at selected time !");
                }
            }
        }
    }
}
