using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IOperationService
    {
        public Operation FindByID(int id);

        public List<Operation> FindByPatientID(int patientId);

        public bool Update(Operation operation);

        public void Remove(Operation operation);

        public void Add(Operation operation);

        public int GetNewID();

        public Appointment FindAppointment(Doctor doctor, Patient patient, DateTime oldDate);

    }
}
