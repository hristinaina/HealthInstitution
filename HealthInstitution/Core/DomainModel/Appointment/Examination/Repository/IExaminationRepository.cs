using System;
using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public interface IExaminationRepository : IRepository
    {
        public List<Examination> FindByPatientID(int patientId);

        public bool Update(Examination examination);

        public int GetNewID();

        public void Add(Examination examination);

        public void Remove(Examination examination);

        public void DeleteByPatientID(int patientId);

        public Appointment FindAppointment(Doctor doctor, Patient patient, DateTime oldDate);
        List<Examination> GetExaminations();
        Examination FindByID(int appointmentID);
    }
}