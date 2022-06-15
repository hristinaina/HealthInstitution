using System.Collections.Generic;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class PatientRepository
    {
        private readonly string _fileName;
        private List<Patient> _patients;
        public List<Patient> Patients { get => _patients; }
        public PatientRepository(string patientFileName)
        {
            _fileName = patientFileName;
            _patients = new List<Patient>();
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

        private bool CheckID(int id)
        {
            foreach (Patient p in _patients)
            {
                if (p.ID == id) return false;
            }
            return true;
        }

        public int GetNewID()
        {
            int i = 1;
            while (true)
            {
                if (CheckID(i)) return i;
                i++;
            }
        }

        public void CreatePatient(string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
        {
            int id = GetNewID();
            Patient patient = new(id, firstName, lastName, email, password, gender, height, weight);
            Patients.Add(patient);
        }
    }
}
