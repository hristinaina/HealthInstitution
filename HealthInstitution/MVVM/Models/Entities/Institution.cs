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

        private PatientRepository _patientRepository;
        private DoctorRepository _doctorRepository;
        private SecretaryRepository _secretaryRepository;
        private AdminRepository _adminRepository;

        private ExaminationRepository _examinationRepository;
        private OperationRepository _operationRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private OperationReferencesRepository _operationReferencesRepository;

        private EquipmentRepository _equipmentRepository;
        private RoomRepository _roomRepository;
        private MedicineRepository _medicineRepository;
        private DayOffRepository _dayOffRepository;
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

            _adminRepository = new AdminRepository(_appSettings.GetAdminFileName());
            _secretaryRepository = new SecretaryRepository(_appSettings.GetSecretaryFileName());
            _patientRepository = new PatientRepository(_appSettings.GetPatientFileName());
            _doctorRepository = new DoctorRepository(_appSettings.GetDoctorFileName());

            _operationRepository = new OperationRepository(_appSettings.GetOperationFileName());
            _examinationRepository = new ExaminationRepository(_appSettings.GetExationFileName());
            _examinationReferencesRepository = new ExaminationReferencesRepository(_appSettings.GetExaminationReferenceFileName());
            _operationReferencesRepository = new OperationReferencesRepository(_appSettings.GetOperationReferenceFileName());

            _dayOffRepository = new DayOffRepository(_appSettings.GetDayOffFileName());
            // TODO: add other repositories

            LoadAll();
            ConnectReferences();
        }

        private void LoadAll()
        {
            _adminRepository.LoadFromFile();
            _patientRepository.LoadFromFile();
            _doctorRepository.LoadFromFile();
            _secretaryRepository.LoadFromFile();
            _examinationRepository.LoadFromFile();
            _operationRepository.LoadFromFile();
            _examinationReferencesRepository.LoadFromFile();
            _operationReferencesRepository.LoadFromFile();
            _dayOffRepository.LoadFromFile();
            // TODO: add other repositories
        }

        public void SaveAll()
        {
            _adminRepository.SaveToFile();
            _patientRepository.SaveToFile();
            _doctorRepository.SaveToFile();
            _secretaryRepository.SaveToFile();
            _examinationRepository.SaveToFile();
            _operationRepository.SaveToFile();
            _examinationReferencesRepository.SaveToFile();
            _operationReferencesRepository.SaveToFile();
            _dayOffRepository.SaveToFile();
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
                Examination examination = _examinationRepository.FindByID(reference.GetExaminationId());
                Doctor doctor = _doctorRepository.FindByID(reference.GetDoctorId());
                Patient patient = _patientRepository.FindByID(reference.GetPatientId());
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
                Operation operation = _operationRepository.FindByID(reference.GetOperationId());
                Doctor doctor = _doctorRepository.FindByID(reference.GetDoctorId());
                Patient patient = _patientRepository.FindByID(reference.GetPatientId());
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
