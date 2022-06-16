using HealthInstitution.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class OperationRepositoryService : IOperationRepositoryService
    {
        IOperationRepository _repository;
        public OperationRepositoryService()
        {
            _repository = Institution.Instance().OperationRepository;
        }

        public void Add(Operation operation)
        {
            _repository.Add(operation);
        }

        public Appointment FindAppointment(Doctor doctor, Patient patient, DateTime oldDate)
        {
            return _repository.FindAppointment(doctor, patient, oldDate);
        }

        public Operation FindByID(int id)
        {
            return _repository.FindByID(id);
        }

        public List<Operation> FindByPatientID(int patientId)
        {
            return _repository.FindByPatientID(patientId);
        }

        public int GetNewID()
        {
            return _repository.GetNewID();
        }

        public List<Operation> GetOperations()
        {
            return _repository.GetOperations();
        }

        public void Remove(Operation operation)
        {
            _repository.Remove(operation);
        }

        public bool Update(Operation operation)
        {
            return _repository.Update(operation);
        }
    }
}
