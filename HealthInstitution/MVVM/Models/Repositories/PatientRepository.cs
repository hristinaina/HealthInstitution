using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class PatientRepository
    {
        private string _patientFileName;
        private List<Patient> _patients;

        public PatientRepository(string patientFileName)
        {
            this._patientFileName = patientFileName;
            this._patients = new List<Patient>();
        }

        public List<Patient> GetPatients()
        {
            return this._patients;
        }

        public void LoadFromFile()
        {
            _patients = FileService.Deserialize<Patient>(_patientFileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Patient>(_patientFileName, _patients);
        }
    }
}
