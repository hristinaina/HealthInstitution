using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Repository
{
    public class PatientRepositoryService : IPatientRepositoryService
    {
        private IPatientRepository _patientRepository;

        public PatientRepositoryService()
        {
            _patientRepository = Institution.Instance().PatientRepository;
        }

        public void CreatePatient(string firstName, string lastName, string email, string password, Gender gender, double height, double weight)
        {
            _patientRepository.CreatePatient(firstName, lastName, email, password, gender, height, weight);
        }

        public Core.Patient FindByID(int id)
        {
            return _patientRepository.FindByID(id);
        }

        public List<Patient> GetPatients()
        {
            return _patientRepository.GetPatients();
        }
    }
}
