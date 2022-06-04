using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models.Services.DoctorServices;

namespace HealthInstitution.MVVM.Models.Services
{
    class ValidationService
    {

        public ValidationService()
        {

        }

        public void ValidateAppointmentData(Patient patient, Doctor doctor, DateTime dateTime, bool validation, int duration = 15)
        {
            User currentUser = Institution.Instance().CurrentUser;
            if (currentUser is Patient || currentUser is Secretary || currentUser is Doctor)
            {
                PatientService patientService = new PatientService(patient);
                DoctorService doctorService = new DoctorService(doctor);
                if (DateTime.Compare(DateTime.Now, dateTime) > 0 && validation)
                {
                    throw new DateException("Date must be in future !");
                }
                if (currentUser is not Doctor)
                {
                    if ((dateTime - DateTime.Now).TotalDays < 1 && validation)
                    {
                        throw new DateException("Cannot schedule in next 24 hours");
                    }
                }
                if (doctor is null)
                {
                    throw new EmptyFieldException("Doctor not selected !");
                }
                if (patient is null)
                {
                    throw new EmptyFieldException("Patient not selected !");
                }
                if (!patientService.IsAvailable(dateTime, duration))
                {
                    throw new UserNotAvailableException("Patient not available at selected time !");
                }
                if (!doctorService.IsAvailable(dateTime, duration))
                {
                    throw new UserNotAvailableException("Doctor not available at selected time !");
                }
            }
        }
    }
}
