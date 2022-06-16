using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services.DoctorServices;

namespace HealthInstitution.Core.Services
{
    class ValidationService
    {

        public ValidationService()
        {

        }

        public void ValidateAppointmentData(Appointment appointment, DateTime datetime, bool validation)
        {
            ExaminationService examinationService = new();
            int duration = examinationService.GetDuration(appointment);

            PatientService patientService = new PatientService(appointment.Patient);
            DoctorService doctorService = new DoctorService(appointment.Doctor);
            if (DateTime.Compare(DateTime.Now, datetime) > 0 && validation)
            {
                throw new DateException("Date must be in future !");
            }
            if (appointment.Doctor is null)
            {
                throw new EmptyFieldException("Doctor not selected !");
            }
            if (appointment.Patient is null)
            {
                throw new EmptyFieldException("Patient not selected !");
            }
            if (!patientService.IsAvailable(datetime, duration))
            {
                throw new UserNotAvailableException("Patient not available at selected time !");
            }
            if (!doctorService.IsAvailable(datetime, duration))
            {
                throw new UserNotAvailableException("Doctor not available at selected time !");
            }
            
        }
    }
}
