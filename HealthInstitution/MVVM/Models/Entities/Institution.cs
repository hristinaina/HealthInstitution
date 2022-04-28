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

        private readonly AllergenRepository _allergenRepository;
        private readonly PatientAllergenRepository _patientAllergenRepository;
        private readonly MedicineAllergenRepository _medicineAllergenRepository;
        private readonly PendingMedicineRepository _pendingMedicineRepository;

        private readonly DoctorDaysOffRepository _doctorDaysOffRepository;
        // TODO: add other repositories

        private static Institution s_instance = null;

        public static Institution Instance()
        {
            if (s_instance is null)
            {
                s_instance = new Institution();
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


            _perscriptionRepository = new PerscriptionRepository(_appSettings.PerscriptionsFileName);
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
            _medicineRepository.LoadFromFile();
            _refferalRepository.LoadFromFile();
            _allergenRepository.LoadFromFile();
            _patientAllergenRepository.LoadFromFile();
            _medicineAllergenRepository.LoadFromFile();
            _pendingMedicineRepository.LoadFromFile();
            _doctorDaysOffRepository.LoadFromFile();
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
        }

        private void ConnectReferences()
        {
            ConnectExaminationReferences();
            ConnectOperationReferences();
            ConnectRefferals();
            FillMedicalRecord();
            ConnectMedicineAllergens();
            ConnectDoctorDaysOff();
        }

        private void ConnectExaminationReferences()
        {
            foreach (ExaminationReference reference in _examinationReferencesRepository.GetReferences())
            {
                Examination examination = _examinationRepository.FindByID(reference.ExaminationID);
                Doctor doctor = _doctorRepository.FindByID(reference.DoctorID);
                Patient patient = _patientRepository.FindByID(reference.PatientID);
                Perscription perscription = _perscriptionRepository.FindByID(reference.PerscriptionID);
                Room room = _roomRepository.FindById(reference.RoomID);


                examination.Doctor = doctor;
                examination.Patient = patient;
                examination.Perscription = perscription;
                examination.Room = room;

                room.Appointments.Add(examination);
                doctor.Examinations.Add(examination);
                patient.Examinations.Add(examination);
            }
        }


        private void ConnectOperationReferences()
        {
            foreach (OperationReference reference in _operationReferencesRepository.GetReferences())
            {
                Operation operation = _operationRepository.FindByID(reference.OperationId);
                Doctor doctor = _doctorRepository.FindByID(reference.DoctorID);
                Patient patient = _patientRepository.FindByID(reference.PatientID);
                Room room = _roomRepository.FindById(reference.RoomID);

                operation.Doctor = doctor;
                operation.Patient = patient;
                operation.Room = room;

                room.Appointments.Add(operation);
                doctor.Operations.Add(operation);
                patient.Operations.Add(operation);
            }
        }

        public void ArrangeEquipment()
        {
            foreach (EquipmentArragment a in this._equipmentArragmentRepository.Equipment)
            {
                Room r = this.RoomRepository.FindById(a.RoomId);
                Equipment e = this.EquipmentRepository.FindById(a.EquipmentId);
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

        public void FillMedicalRecord()
        {
            foreach (Patient patient in _patientRepository.GetPatients())
            {
                patient.Examinations = _examinationRepository.FindByPatientID(patient.ID);
                patient.Operations = _operationRepository.FindByPatientID(patient.ID);

                List<PatientAllergen> patientAllergens = _patientAllergenRepository.FindByPatientID(patient.ID);
                patient.Record.Allergens = _allergenRepository.PatientAllergenToAllergen(patientAllergens);
            }
        }

        public void ConnectMedicineAllergens()
        {
            foreach (Medicine medicine in _medicineRepository.Medicine)
            {
                List<MedicineAllergen> medicineAllergens = _medicineAllergenRepository.FindByMedicineID(medicine.ID);
                medicine.Allergens = _allergenRepository.MedicineAllergenToAllergen(medicineAllergens);
            }
        }

        public void ConnectDoctorDaysOff()
        {
            foreach (Doctor doctor in _doctorRepository.GetDoctors())
            {
                List<DoctorDaysOff> doctorDaysOff = _doctorDaysOffRepository.FindByDoctorID(doctor.ID);
                doctor.DaysOff = _dayOffRepository.DoctorDaysOffToDaysOff(doctorDaysOff);

                foreach(DayOff dayOff in doctor.DaysOff) dayOff.Doctor = doctor;
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
        public AllergenRepository AllergenRepository { get => _allergenRepository; }
        public PatientAllergenRepository PatientAllergenRepository { get => _patientAllergenRepository; }
        public MedicineAllergenRepository MedicineAllergenRepository { get => _medicineAllergenRepository; }
        public PendingMedicineRepository PendingMedicineRepository { get => _pendingMedicineRepository; }
        public DoctorDaysOffRepository DoctorDaysOffRepository { get => _doctorDaysOffRepository; }


    }
}
