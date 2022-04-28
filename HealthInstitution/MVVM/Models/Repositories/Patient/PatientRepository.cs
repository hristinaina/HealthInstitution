using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class PatientRepository
    {
        private string _fileName;
        private List<Patient> _patients;

        public PatientRepository(string patientFileName)
        {
            this._fileName = patientFileName;
            this._patients = new List<Patient>();
        }

        public List<Patient> GetPatients()
        {
            return this._patients;
        }

        public void LoadFromFile()
        {
            _patients = FileService.Deserialize<Patient>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Patient>(_fileName, _patients);
        }
        public Patient FindByID(int id)
        {
            foreach (Patient patient in _patients)
            {
                if (patient.ID == id) return patient;
            }
            return null;
        }
    }
}
