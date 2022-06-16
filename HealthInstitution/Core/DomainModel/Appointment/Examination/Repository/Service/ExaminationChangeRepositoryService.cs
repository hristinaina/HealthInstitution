using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class ExaminationChangeRepositoryService : IExaminationChangeRepositoryService
    {
        IExaminationChangeRepository _repository;

        public ExaminationChangeRepositoryService()
        {
            _repository = Institution.Instance().ExaminationChangeRepository;
        }

        public void Add(Examination examination, DateTime dateTime, bool resolved, AppointmentStatus status)
        {
            _repository.Add(examination, dateTime, resolved, status);
        }

        public void DeleteUnresolvedRequestsByPatientId(int patientId)
        {
            _repository.DeleteUnresolvedRequestsByPatientId(patientId);
        }

        public ExaminationChange FindByAppointmentID(int id)
        {
            return _repository.FindByAppointmentID(id);
        }

        public ExaminationChange FindByID(int id)
        {
            return _repository.FindByID(id);
        }

        public List<ExaminationChange> GetChanges()
        {
            return _repository.GetChanges();
        }

        public int GetNewID()
        {
            return _repository.GetNewID();
        }

        public void RemoveByAppointmentId(int appointmentId)
        {
            _repository.RemoveByAppointmentId(appointmentId);
        }
    }
}
