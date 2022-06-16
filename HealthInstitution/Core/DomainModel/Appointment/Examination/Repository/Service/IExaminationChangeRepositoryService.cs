using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IExaminationChangeRepositoryService
    {
        public ExaminationChange FindByAppointmentID(int id);

        public int GetNewID();

        public void Add(Examination examination, DateTime dateTime, bool resolved, AppointmentStatus status);

        public ExaminationChange FindByID(int id);

        public void DeleteUnresolvedRequestsByPatientId(int patientId);

        public void RemoveByAppointmentId(int appointmentId);
    }
}
