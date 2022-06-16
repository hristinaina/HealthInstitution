using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class PatientAllergenRepositoryService : IPatientAllergenRepositoryService
    {
        private IPatientAllergenRepository _patientAllergenRepository;

        public PatientAllergenRepositoryService()
        {
            _patientAllergenRepository = Institution.Instance().PatientAllergenRepository;
        }

        public bool Add(Allergen allergen, Patient patient)
        {
            return _patientAllergenRepository.Add(allergen, patient);
        }

        public List<PatientAllergen> FindByPatientID(int patientId)
        {
            return _patientAllergenRepository.FindByPatientID(patientId);
        }
    }
}
