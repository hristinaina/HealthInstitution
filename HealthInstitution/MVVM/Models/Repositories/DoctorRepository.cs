using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities.Doctor;

namespace HealthInstitution.Repositories
{
    class DoctorRepository
    {
        private string _doctorFileName;
        private List<Doctor> _doctors;

        public DoctorRepository(string doctorFileName)
        {
            this._doctorFileName = doctorFileName;
            this._doctors = new List<Doctor>();
        }

        public List<Doctor> GetDoctors()
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
