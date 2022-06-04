using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.Models.Services
{
    class ExaminationService
    {
        private readonly ExaminationRepository _examinationRepository;

        public ExaminationService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
        }
        public void AddPrescription(Examination examination, Entities.Prescription prescription)
        {
            examination.Prescriptions.Add(prescription);
            //_examinationRepository.Update(examination);
        }

        public bool AddAnamnesis(Examination examination, string anamnesis)
        {
            foreach (Examination i in _examinationRepository.Examinations)
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
            int duration = 0;
            Type type = appointment.GetType();
            if (type.Equals(typeof(Examination))) duration = 15;
            else
            {
                Operation operation = (Operation)appointment;
                duration = operation.Duration;
            }

            return duration;
        }

        public List<Examination> GetFutureExaminations(Specialization specialization, Patient patient)
        {
            List<Examination> futureAppointments = new();
            foreach (Examination appointment in _examinationRepository.Examinations)
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
