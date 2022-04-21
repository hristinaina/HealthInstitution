using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.Repositories
{
    public class DoctorRepository
    {
        private string _doctorFileName;
        private List<DoctorController> _doctors;

        public DoctorRepository(string doctorFileName)
        {
            this._doctorFileName = doctorFileName;
            this._doctors = new List<DoctorController>();
        }

        public List<DoctorController> GetDoctors()
        {
            return this._doctors;
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
