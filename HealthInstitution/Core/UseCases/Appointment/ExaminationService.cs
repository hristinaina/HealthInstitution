using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repository;
using HealthInstitution.Desktop.MVVM.Models.Services.Appointment;

namespace HealthInstitution.Core.Services
{
    class ExaminationService : AppointmentService
    {
        private readonly IExaminationRepositoryService _examinationRepository;

        public ExaminationService()
        {
            _examinationRepository = new ExaminationRepositoryService();
        }
        public void AddPrescription(Examination examination, Prescription prescription)
        {
            examination.Prescriptions.Add(prescription);
            //_examinationRepository.Update(examination);
        }

        public bool AddAnamnesis(Examination examination, string anamnesis)
        {
            foreach (Examination i in _examinationRepository.GetExaminations())
            {
                if (i.ID == examination.ID)
                {
                    examination.Anamnesis = anamnesis;
                    return true;
                }
            }
            return false;
        }

        public int GetDuration(Appointment appointment)
        {
            int duration = 15;
            if (appointment.GetType() == typeof(Operation))
            {
                Operation o = (Operation)appointment;
                duration = o.Duration;
            }
            return duration;
        }

        public List<Examination> GetFutureExaminations(Specialization specialization, Patient patient)
        {
            List<Examination> futureAppointments = new();
            foreach (Examination appointment in _examinationRepository.GetExaminations())
            {
                if (DateTime.Compare(appointment.Date, DateTime.Now) > 0 &&
                    (appointment.Doctor.Specialization == specialization || appointment.Patient == patient))
                {
                    futureAppointments.Add(appointment);
                }
            }
            futureAppointments = futureAppointments.OrderBy(x => x.Date).ToList();
            return futureAppointments;
        }

    }
}
