using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IOperationRepository
    {
        public Operation FindByID(int id);

        public List<Operation> FindByPatientID(int patientId);

        public bool Update(Operation operation);

        public void Remove(Operation operation);

        public void Add(Operation operation);

        public int GetID();

        public Appointment FindAppointment(Doctor doctor, Patient patient, DateTime oldDate);
    }
}