using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities.Patient;

namespace HealthInstitution.MVVM.Models.Repositories
{
    class PatientRepository
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

        public bool LoadFromFile()
        {
            // TODO: implementirati funkciju za ucitavanje podataka iz fajla
            return false;
        }

        public bool SaveToFile()
        {
            // TODO: implementirati funkciju za cuvanje podataka u fajl
            return false;
        }
    }
}
