using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.Repositories;
using System;
using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.Models.Repositories.Room;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Repositories.References;


namespace HealthInstitution.MVVM.Models
{
    // class through which all system entities can be accessed
    // implemented using Singleton pattern
    public sealed class Institution
    {
        private readonly AppSettings _appSettings;

        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly SecretaryRepository _secretaryRepository;
        private readonly AdminRepository _adminRepository;

        private readonly PerscriptionRepository _perscriptionRepository;
        private readonly ExaminationRepository _examinationRepository;
        private readonly OperationRepository _operationRepository;
        private readonly ExaminationReferencesRepository _examinationReferencesRepository;
        private readonly OperationReferencesRepository _operationReferencesRepository;

        private readonly EquipmentRepository _equipmentRepository;
        private readonly EquipmentArragmentRepository _equipmentArragmentRepository;
        private readonly RoomRepository _roomRepository;
        private readonly MedicineRepository _medicineRepository;
        private readonly DayOffRepository _dayOffRepository;
        private readonly RefferalRepository _refferalRepository;
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

            _perscriptionRepository = new PerscriptionRepository(_appSettings.GetPerscriptionFileName());
            _operationRepository = new OperationRepository(_appSettings.GetOperationFileName());
            _examinationRepository = new ExaminationRepository(_appSettings.GetExationFileName());
            _examinationReferencesRepository = new ExaminationReferencesRepository(_appSettings.GetExaminationReferenceFileName());
            _operationReferencesRepository = new OperationReferencesRepository(_appSettings.GetOperationReferenceFileName());
            _roomRepository = new RoomRepository(_appSettings.GetRoomFileName());
            _equipmentRepository = new EquipmentRepository(_appSettings.GetEquipmentFileName());
            _equipmentArragmentRepository = new EquipmentArragmentRepository(_appSettings.GetEquipmentArragmentFileName());

            _dayOffRepository = new DayOffRepository(_appSettings.GetDayOffFileName());
            _refferalRepository = new RefferalRepository(_appSettings.GetRefferalFileName());
            // TODO: add other repositories

            LoadAll();
            ConnectReferences();
        }

        public void LoadAll()
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
            _roomRepository.LoadFromFile();
            _equipmentRepository.LoadFromFile();
            _refferalRepository.LoadFromFile();
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
            _roomRepository.SaveToFile();
            _equipmentRepository.SaveToFile();
            _refferalRepository.SaveToFile();
        }

        private void ConnectReferences()
        {
            ConnectExaminationReferences();
            ConnectOperationReferences();
            ArrangeEquipment();
            ConnectRefferals();
        }

        private void ConnectExaminationReferences()
        {
            foreach (ExaminationReference reference in _examinationReferencesRepository.GetReferences())
            {
                Examination examination = _examinationRepository.FindByID(reference.ExaminationID);
                Doctor doctor = _doctorRepository.FindByID(reference.DoctorID);
                Patient patient = _patientRepository.FindByID(reference.PatientID);
                Perscription perscription = _perscriptionRepository.FindByID(reference.PerscriptionID);
                // TODO -- room


                examination.Doctor = doctor;
                examination.Patient = patient;
                examination.Perscription = perscription;
                // TODO -- set room

                doctor.Examinations.Add(examination);
                patient.GetExaminations().Add(examination);
            }
        }


        private void ConnectOperationReferences()
        {
            foreach (OperationReference reference in _operationReferencesRepository.GetReferences())
            {
                Operation operation = _operationRepository.FindByID(reference.OperationId);
                Doctor doctor = _doctorRepository.FindByID(reference.DoctorID);
                Patient patient = _patientRepository.FindByID(reference.PatientID);
                // TODO -- room

                operation.Doctor = doctor;
                operation.Patient = patient;
                // TODO -- set room

                doctor.Operations.Add(operation);
                patient.GetOperations().Add(operation);
            }
        }

        public void ArrangeEquipment()
        {
            foreach (EquipmentArragment a in this._equipmentArragmentRepository.Equipment)
            {
                Room r = this.RoomRepository.GetById(a.RoomId);
                Equipment e = this.EquipmentRepository.GetById(a.EquipmentId);
                r.AddEquipment(e, a.Quantity);
                e.ArrangeInRoom(r, a.Quantity);
            }
        }

        public void ConnectRefferals()
        {
            foreach (Refferal reference in _refferalRepository.GetReferences())
            {
                Patient patient = _patientRepository.FindByID(reference.PatientId);
                
                // finds all refferals for corresponding patient
                patient.Record.Refferals = _refferalRepository.FindByPatientID(patient.ID);
            }
        }

        public PatientRepository PatientRepository { get => _patientRepository; }
        public DoctorRepository DoctorRepository { get => _doctorRepository; }
        public SecretaryRepository SecretaryRepository { get => _secretaryRepository; }
        public AdminRepository AdminRepository { get => _adminRepository; }
        public PerscriptionRepository PerscriptionRepository { get => _perscriptionRepository; }
        public ExaminationRepository ExaminationRepository { get => _examinationRepository; }
        public OperationRepository OperationRepository { get => _operationRepository; }
        public ExaminationReferencesRepository ExaminationReferencesRepository { get => _examinationReferencesRepository; }
        public OperationReferencesRepository OperationReferencesRepository { get => _operationReferencesRepository; }
        public EquipmentRepository EquipmentRepository { get => _equipmentRepository; }
        public RoomRepository RoomRepository { get => _roomRepository; }
        public MedicineRepository MedicineRepository { get => _medicineRepository; }
        public DayOffRepository DayOffRepository { get => _dayOffRepository; }
        public RefferalRepository RefferalRepository { get => _refferalRepository; }
    }
}
