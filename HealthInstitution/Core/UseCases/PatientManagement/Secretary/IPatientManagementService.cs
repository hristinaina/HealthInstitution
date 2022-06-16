using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.PatientManagement
{
    public interface IPatientManagementService
    {
        public void CreatePatient(string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight);

        public void UpdatePatient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight);

        public void DeletePatient(int id);

        public void BlockPatient(int id);

        public void UnblockPatient(int id);

        public void AddIllness(Patient patient, string illness);

        public bool IsAllergic(Patient patient, List<Allergen> allergens);
    }
}
