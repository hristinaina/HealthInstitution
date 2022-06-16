using System;

namespace HealthInstitution.Core
{
    public interface IExaminationChangeRepository
    {
        public ExaminationChange FindByAppointmentID(int id);

        public int NewId();

        public void Add(Examination examination, DateTime dateTime, bool resolved, AppointmentStatus status);

        public ExaminationChange FindByID(int id);

        public void DeleteUnresolvedRequestsByPatientId(int patientId);

        public void RemoveByAppointmentId(int appointmentId);
    }
}