using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.Exceptions;

namespace HealthInstitution.MVVM.Models.Services
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
            if (!appointment.Patient.IsAvailable(datetime, duration))
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
