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
        private AppSettings _appSettings;

        public PatientRepository PatientRepository;
        public DoctorRepository DoctorRepository;
        public SecretaryRepository SecretaryRepository;
        public AdminRepository AdminRepository;

        public ExaminationRepository ExaminationRepository;
        public OperationRepository OperationRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private OperationReferencesRepository _operationReferencesRepository;

        public EquipmentRepository EquipmentRepository;
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
            _appSettings = AppSettings.Instance();

            AdminRepository = new AdminRepository(_appSettings.GetAdminFileName());
            SecretaryRepository = new SecretaryRepository(_appSettings.GetSecretaryFileName());
            PatientRepository = new PatientRepository(_appSettings.GetPatientFileName());
            DoctorRepository = new DoctorRepository(_appSettings.GetDoctorFileName());

            OperationRepository = new OperationRepository(_appSettings.GetOperationFileName());
            ExaminationRepository = new ExaminationRepository(_appSettings.GetExationFileName());
            _examinationReferencesRepository = new ExaminationReferencesRepository(_appSettings.GetExaminationReferenceFileName());
            _operationReferencesRepository = new OperationReferencesRepository(_appSettings.GetOperationReferenceFileName());

            DayOffRepository = new DayOffRepository(_appSettings.GetDayOffFileName());
            // TODO: add other repositories

            LoadAll();
            ConnectReferences();
        }

        private void LoadAll()
        {
            AdminRepository.LoadFromFile();
            PatientRepository.LoadFromFile();
            DoctorRepository.LoadFromFile();
            SecretaryRepository.LoadFromFile();
            ExaminationRepository.LoadFromFile();
            OperationRepository.LoadFromFile();
            _examinationReferencesRepository.LoadFromFile();
            _operationReferencesRepository.LoadFromFile();
            DayOffRepository.LoadFromFile();
            // TODO: add other repositories
        }

        public void SaveAll()
        {
            AdminRepository.SaveToFile();
            PatientRepository.SaveToFile();
            DoctorRepository.SaveToFile();
            SecretaryRepository.SaveToFile();
            ExaminationRepository.SaveToFile();
            OperationRepository.SaveToFile();
            _examinationReferencesRepository.SaveToFile();
            _operationReferencesRepository.SaveToFile();
            DayOffRepository.SaveToFile();
            // TODO: add other repositories
        }

        private void ConnectReferences()
        {
            ConnectExaminationReferences();
            ConnectExaminationReferences();

        }

        private void ConnectExaminationReferences()
        {
            foreach (ExaminationReference reference in _examinationReferencesRepository.GetReferences())
            {
                Examination examination = ExaminationRepository.FindByID(reference.GetExaminationId());
                Doctor doctor = DoctorRepository.FindByID(reference.GetDoctorId());
                Patient patient = PatientRepository.FindByID(reference.GetPatientId());
                // TODO -- room
                // TODO -- perscription

                examination.SetDoctor(doctor);
                examination.SetPatient(patient);
                // TODO -- set room
                // TODO -- set perscription

                doctor.GetExaminations().Add(examination);
                patient.GetExaminations().Add(examination);
            }
        }


        private void ConnectOperationReferences()
        {
            foreach (OperationReference reference in _operationReferencesRepository.GetReferences())
            {
                Operation operation = OperationRepository.FindByID(reference.GetOperationId());
                Doctor doctor = DoctorRepository.FindByID(reference.GetDoctorId());
                Patient patient = PatientRepository.FindByID(reference.GetPatientId());
                // TODO -- room

                operation.SetDoctor(doctor);
                operation.SetPatient(patient);
                // TODO -- set room

                doctor.GetOperations().Add(operation);
                patient.GetOperations().Add(operation);
            }
        }
    }
}
