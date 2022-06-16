using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IExaminationRepositoryService
    {
        public List<Examination> FindByPatientID(int patientId);

        public bool Update(Examination examination);

        public int GetNewID();

        public void Add(Examination examination);

        public void Remove(Examination examination);

        public void DeleteByPatientID(int patientId);

        public Appointment FindAppointment(Doctor doctor, Patient patient, DateTime oldDate);

    }
}
