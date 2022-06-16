using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IPatientAllergenRepository
    {
        public List<PatientAllergen> FindByPatientID(int patientId);

        public bool Add(Allergen allergen, Patient patient);
    }
}