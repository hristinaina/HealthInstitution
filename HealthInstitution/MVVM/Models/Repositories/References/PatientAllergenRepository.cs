using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories.References
{
    public class PatientAllergenRepository
    {
        private string _fileName;
        private List<PatientAllergen> _references;

        public PatientAllergenRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<PatientAllergen>();
        }
        public List<PatientAllergen> GetReferences()
        {
            return _references;
        }

        public void LoadFromFile()
        {
            _references = FileService.Deserialize<PatientAllergen>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<PatientAllergen>(_fileName, _references);
        }

        public List<PatientAllergen> FindByPatientID(int patientId)
        {
            List<PatientAllergen> patientAllergens = new();
            foreach (PatientAllergen reference in _references)
            {
                if (reference.PatientId == patientId)
                    patientAllergens.Add(reference);
            }
            return patientAllergens;
        }
    }
}
