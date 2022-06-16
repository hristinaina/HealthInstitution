using System;
using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public interface IExaminationChangeRepository : IRepository
    {
        public ExaminationChange FindByAppointmentID(int id);

        public int GetNewID();

        public void Add(Examination examination, DateTime dateTime, bool resolved, AppointmentStatus status);

        public ExaminationChange FindByID(int id);

        public void DeleteUnresolvedRequestsByPatientId(int patientId);

        public void RemoveByAppointmentId(int appointmentId);
        List<ExaminationChange> GetChanges();
    }
}