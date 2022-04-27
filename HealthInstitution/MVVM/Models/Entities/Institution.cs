using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.Repositories;
using System;
using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Services;

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
        public ExaminationRepository ExaminationRepository;
        public EquipmentRepository EquipmentRepository;
        public OperationRepository OperationRepository;
        public RoomRepository RoomRepository;
        public MedicineRepository MedicineRepository;
        public DayOffRepository DayOffRepository;
        // TODO: add other repositories

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
            OperationRepository = new OperationRepository(AppSettings.Instance().GetOperationFileName());
            ExaminationRepository = new ExaminationRepository(AppSettings.Instance().GetOperationFileName());
            DayOffRepository = new DayOffRepository(AppSettings.Instance().GetDayOffFileName());
            // TODO: add other repositories
        }

        public void LoadAll()
        {
            AdminRepository.LoadFromFile();
            PatientRepository.LoadFromFile();
            DoctorRepository.LoadFromFile();
            SecretaryRepository.LoadFromFile();
            OperationRepository.LoadFromFile();
            ExaminationRepository.LoadFromFile();
            DayOffRepository.LoadFromFile();
            // TODO: add other repositories
        }

        public void SaveAll()
        {
            AdminRepository.SaveToFile();
            PatientRepository.SaveToFile();
            DoctorRepository.SaveToFile();
            SecretaryRepository.SaveToFile();
            OperationRepository.SaveToFile();
            ExaminationRepository.SaveToFile();
            DayOffRepository.SaveToFile();
            // TODO: add other repositories
        }
    }
}
