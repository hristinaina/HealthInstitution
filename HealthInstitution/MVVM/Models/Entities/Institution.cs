using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.Repositories;
using System;
using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.Models.Repositories.Room;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Repositories.References;
using HealthInstitution.MVVM.Models.Enumerations;

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

        private readonly PrescriptionRepository _prescriptionRepository;
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

        private readonly AllergenRepository _allergenRepository;
        private readonly PatientAllergenRepository _patientAllergenRepository;
        private readonly MedicineAllergenRepository _medicineAllergenRepository;
        private readonly PendingMedicineRepository _pendingMedicineRepository;

        private readonly DoctorDaysOffRepository _doctorDaysOffRepository;
        private readonly PrescriptionMedicineRepository _prescriptionMedicineRepository;
        private ExaminationChangeRepository _examinationChangeRepository;
        // TODO: add other repositories
        private User _currentUser;
        public User CurrentUser { get => _currentUser; set { _currentUser = value; } }

        private static Institution s_instance = null;

        public static Institution Instance()
        {
            if (s_instance is null)
            {
                s_instance = new Institution();
                ConnectReferences();
            }
            return s_instance;
        }

        private Institution()
        {
            _appSettings = AppSettings.Instance();
            _adminRepository = new AdminRepository(_appSettings.AdminsFileName);
            _secretaryRepository = new SecretaryRepository(_appSettings.SecretariesFileName);
            _patientRepository = new PatientRepository(_appSettings.PatientsFileName);
            _doctorRepository = new DoctorRepository(_appSettings.DoctorsFileName);


            _prescriptionRepository = new PrescriptionRepository(_appSettings.PerscriptionsFileName);
            _examinationRepository = new ExaminationRepository(_appSettings.ExaminationsFileName);
            _operationRepository = new OperationRepository(_appSettings.OperationsFileName);
            _examinationReferencesRepository = new ExaminationReferencesRepository(_appSettings.ExaminationReferencesFileName);
            _operationReferencesRepository = new OperationReferencesRepository(_appSettings.OperationsReferencesFileName);
            _roomRepository = new RoomRepository(_appSettings.RoomsFileName);
            _equipmentRepository = new EquipmentRepository(_appSettings.EquipmentFileName);

            _dayOffRepository = new DayOffRepository(_appSettings.DaysOffFileName);
            _refferalRepository = new RefferalRepository(_appSettings.RefferalsFileName);

            _equipmentArragmentRepository = new EquipmentArragmentRepository(_appSettings.EquipmentArrangementFileName);
            _medicineRepository = new MedicineRepository(_appSettings.MedicinesFileName);
            _allergenRepository = new AllergenRepository(_appSettings.AllergensFileName);
            _patientAllergenRepository = new PatientAllergenRepository(_appSettings.PatientAllergensFileName);
            _medicineAllergenRepository = new MedicineAllergenRepository(_appSettings.MedicineAllergensFileName);
            _pendingMedicineRepository = new PendingMedicineRepository(_appSettings.PendingMedicinesFileName);

            _doctorDaysOffRepository = new DoctorDaysOffRepository(_appSettings.DoctorDaysOffFileName);
            _prescriptionMedicineRepository = new PrescriptionMedicineRepository(_appSettings.PrescriptionMedicineFileName);
            _examinationChangeRepository = new ExaminationChangeRepository(_appSettings.ExaminationChangeFileName);
            // TODO: add other repositories

            LoadAll();
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
            _medicineRepository.LoadFromFile();
            _refferalRepository.LoadFromFile();
            _allergenRepository.LoadFromFile();
            _patientAllergenRepository.LoadFromFile();
            _medicineAllergenRepository.LoadFromFile();
            _pendingMedicineRepository.LoadFromFile();
            _doctorDaysOffRepository.LoadFromFile();
            _prescriptionMedicineRepository.LoadFromFile();
            _examinationChangeRepository.LoadFromFile();
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
            _medicineRepository.SaveToFile();
            _allergenRepository.SaveToFile();
            _patientAllergenRepository.SaveToFile();
            _medicineAllergenRepository.LoadFromFile();
            _pendingMedicineRepository.SaveToFile();
            _doctorDaysOffRepository.SaveToFile();
            _prescriptionMedicineRepository.SaveToFile();
            // TODO: Add other repositories
        }

        private static void ConnectReferences()
        {
            ReferencesService.ConnectExaminationReferences();
            ReferencesService.ConnectOperationReferences();
            ReferencesService.ConnectRefferals();
            ReferencesService.FillMedicalRecord();
            ReferencesService.ConnectMedicineAllergens();
            ReferencesService.ConnectDoctorDaysOff();
            ReferencesService.ConnectPrescriptionRepository();
            ReferencesService.ConnectExaminationChanges();
        }

        internal void CreateAppointment()
        {
        }

        public PatientRepository PatientRepository { get => _patientRepository; }
        public DoctorRepository DoctorRepository { get => _doctorRepository; }
        public SecretaryRepository SecretaryRepository { get => _secretaryRepository; }
        public AdminRepository AdminRepository { get => _adminRepository; }
        public PrescriptionRepository PrescriptionRepository { get => _prescriptionRepository; }
        public ExaminationRepository ExaminationRepository { get => _examinationRepository; }
        public OperationRepository OperationRepository { get => _operationRepository; }
        public ExaminationReferencesRepository ExaminationReferencesRepository { get => _examinationReferencesRepository; }
        public OperationReferencesRepository OperationReferencesRepository { get => _operationReferencesRepository; }
        public EquipmentRepository EquipmentRepository { get => _equipmentRepository; }
        public RoomRepository RoomRepository { get => _roomRepository; }
        public MedicineRepository MedicineRepository { get => _medicineRepository; }
        public DayOffRepository DayOffRepository { get => _dayOffRepository; }
        public RefferalRepository RefferalRepository { get => _refferalRepository; }
        public AllergenRepository AllergenRepository { get => _allergenRepository; }
        public PatientAllergenRepository PatientAllergenRepository { get => _patientAllergenRepository; }
        public MedicineAllergenRepository MedicineAllergenRepository { get => _medicineAllergenRepository; }
        public PendingMedicineRepository PendingMedicineRepository { get => _pendingMedicineRepository; }
        public DoctorDaysOffRepository DoctorDaysOffRepository { get => _doctorDaysOffRepository; }
        public PrescriptionMedicineRepository PrescriptionMedicineRepository { get => _prescriptionMedicineRepository; }
        public ExaminationChangeRepository ExaminationChangeRepository { get => _examinationChangeRepository; }
        public EquipmentArragmentRepository EquipmentArragmentRepository { get => _equipmentArragmentRepository; }


        public Examination CreateExamination(Doctor doctor, Patient patient, DateTime datetime)
        {
            if (!doctor.IsAvailable(datetime)) {
                return null;
            }
            if (!patient.IsAvailable(datetime)) {
                return null;
            }
            int appointmentId = _examinationRepository.NewId();
            int prescriptionId = _prescriptionRepository.NewId();
            Prescription prescription = new Prescription(prescriptionId);
            Examination examination = new Examination(appointmentId, doctor, patient, datetime, prescription);
            patient.Examinations.Add(examination);
            doctor.Examinations.Add(examination);
            // find a room
            // add examination to room
            // add room to examination
            _examinationRepository.Add(examination);
            _examinationReferencesRepository.Add(examination);
            _examinationChangeRepository.Add(examination, true, AppointmentStatus.CREATED);

            return examination;
        }


        public void RescheduleExamination(Examination examination, DateTime datetime)
        {
            if (examination.Doctor.IsAvailable(datetime)) {
                return;
            }
            if (examination.Patient.IsAvailable(datetime)) {
                return;
            }
            examination.Date = datetime;
            // check room
            // if add examination to room
            // if add room to examination
            //_examinationReferencesRepository.Add(examination);
            bool resolved = examination.IsEditable();
            _examinationChangeRepository.Add(examination, resolved, AppointmentStatus.EDITED);

        }


        public void CancelExamination(Examination examination)
        {

            bool resolved = examination.IsEditable();
            if (!resolved)
            {
                _examinationChangeRepository.Add(examination, resolved, AppointmentStatus.DELETED);
                return;
            }
            Patient patient = examination.Patient;
            Doctor doctor = examination.Doctor;
            Room room = examination.Room;
            patient.Examinations.Remove(examination);
            doctor.Examinations.Remove(examination);
            // remove from room
            _examinationRepository.Remove(examination);
            _examinationReferencesRepository.Remove(examination);

        }
    }
}
