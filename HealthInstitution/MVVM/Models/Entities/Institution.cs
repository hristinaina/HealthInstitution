using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.Repositories;
using System;

namespace HealthInstitution.MVVM.Models
{
    // class through which all system entities can be accessed
    // implemented using Singleton pattern
    public sealed class Institution
    {
        public PatientRepository PatientRepository;
        public DoctorRepository DoctorRepository;
        public SecretaryRepository SecretaryRepository;
        public AdminRepository AdminRepository;

        // TODO: add other repositories
        private User _currentUser;
        public User CurrentUser { get => _currentUser; set { _currentUser = value; } }

        private static Institution s_instance = null;

        public static Institution Instance()
        {
            if (s_instance == null)
            {
                s_instance = new Institution();
            }
            return s_instance;
        }

        private Institution()
        {
            AdminRepository = new AdminRepository(AppSettings.Instance().GetAdminFileName());
            SecretaryRepository = new SecretaryRepository(AppSettings.Instance().GetSecretaryFileName());
            PatientRepository = new PatientRepository(AppSettings.Instance().GetPatientFileName());
            DoctorRepository = new DoctorRepository(AppSettings.Instance().GetDoctorFileName());
            // TODO: add other repositories
        }

        public void LoadAll()
        {
            AdminRepository.LoadFromFile();
            PatientRepository.LoadFromFile();
            DoctorRepository.LoadFromFile();
            SecretaryRepository.LoadFromFile();
            // TODO: add other repositories
        }

        public void SaveAll()
        {
            AdminRepository.SaveToFile();
            PatientRepository.SaveToFile();
            DoctorRepository.SaveToFile();
            SecretaryRepository.SaveToFile();
            // TODO: add other repositories
        }
    }
}
