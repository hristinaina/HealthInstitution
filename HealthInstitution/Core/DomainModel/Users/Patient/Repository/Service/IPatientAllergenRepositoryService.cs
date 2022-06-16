using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IPatientAllergenRepositoryService
    {
        public List<PatientAllergen> FindByPatientID(int patientId);

        public bool Add(Allergen allergen, Patient patient);
    }
}
