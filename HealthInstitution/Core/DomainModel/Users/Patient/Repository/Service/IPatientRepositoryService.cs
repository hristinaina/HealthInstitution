using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    interface IPatientRepositoryService
    {
        public Patient FindByID(int id);


        public void CreatePatient(string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight);

        public List<Patient> GetPatients();
    }
}
