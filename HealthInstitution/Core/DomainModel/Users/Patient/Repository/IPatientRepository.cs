using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IPatientRepository : IRepository
    {
        public Patient FindByID(int id);

        public void CreatePatient(string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight);

        public List<Patient> GetPatients();
    }
}
