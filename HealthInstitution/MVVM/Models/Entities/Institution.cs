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
using HealthInstitution.Exceptions;

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
        private readonly EquipmentArrangementRepository _equipmentArragmentRepository;
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
            _equipmentArragmentRepository = new EquipmentArrangementRepository(_appSettings.EquipmentArrangementFileName);

            _dayOffRepository = new DayOffRepository(_appSettings.DaysOffFileName);
            _refferalRepository = new RefferalRepository(_appSettings.RefferalsFileName);

            _equipmentArragmentRepository = new EquipmentArrangementRepository(_appSettings.EquipmentArrangementFileName);
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
            _equipmentArragmentRepository.LoadFromFile();
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
            _equipmentArragmentRepository.SaveToFile();
            _refferalRepository.SaveToFile();
            _medicineRepository.SaveToFile();
            _allergenRepository.SaveToFile();
            _patientAllergenRepository.SaveToFile();
            _medicineAllergenRepository.LoadFromFile();
            _pendingMedicineRepository.SaveToFile();
            _doctorDaysOffRepository.SaveToFile();
            _prescriptionMedicineRepository.SaveToFile();
            _examinationChangeRepository.SaveToFile();
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
            ReferencesService.ArrangeEquipment();
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
        public EquipmentArrangementRepository EquipmentArragmentRepository { get => _equipmentArragmentRepository; }


        public bool CreateAppointment(Doctor doctor, Patient patient, DateTime dateTime, string type, int duration = 15)
        {
            if (CurrentUser is Patient && patient.IsTrolling())
            {
                throw new PatientBlockedException("System has blocked your account !");
            }
            if (CurrentUser is Doctor)
            {
                if (!doctor.IsAvailable(dateTime))
                {
                    return false;
                }
                if (!patient.IsAvailable(dateTime))
                {
                    return false;
                }
            }
            ValidateAppointmentData(patient, doctor, dateTime);

            int appointmentId = 0;

            if (type == nameof(Examination))
            {

                appointmentId = _examinationRepository.NewId();
                int prescriptionId = _prescriptionRepository.NewId();
                Prescription prescription = new Prescription(prescriptionId);

                Examination examination = new Examination(appointmentId, doctor, patient, dateTime, prescription);
                patient.Examinations.Add(examination);
                doctor.Examinations.Add(examination);
                _roomRepository.FindAvailableRoom(examination, dateTime);
                _examinationRepository.Add(examination);
                _examinationReferencesRepository.Add(examination);
                _examinationChangeRepository.Add(examination, dateTime, true, AppointmentStatus.CREATED);

            }

            else if (type == nameof(Operation)) {
                appointmentId = _operationRepository.NewId();
                Operation operation = new Operation(appointmentId, doctor, patient, dateTime, duration);
                patient.Operations.Add(operation);
                doctor.Operations.Add(operation);
                _roomRepository.FindAvailableRoom(operation, dateTime);
                _operationRepository.Add(operation);
                _operationReferencesRepository.Add(operation);

            }

            return true;
        }


        public bool RescheduleExamination(Appointment appointment, DateTime dateTime)
        {
            if (CurrentUser is Patient && appointment.Patient.IsTrolling())
            {
                throw new PatientBlockedException("System has blocked your account !");
            }
            if (CurrentUser is Doctor)
            {
                if (!appointment.Doctor.IsAvailable(dateTime))
                {
                    return false;
                }
                if (!appointment.Patient.IsAvailable(dateTime))
                {
                    return false;
                }
            }
            ValidateAppointmentData(appointment.Patient, appointment.Doctor, dateTime);

            _roomRepository.FindAvailableRoom(appointment, dateTime);
            bool resolved = true;
            if (CurrentUser is Patient) {
                resolved = appointment.IsEditable();
            }
            if (resolved) {
                appointment.Date = dateTime;
            }

            if (appointment is Examination)
            {
                
                _examinationReferencesRepository.Remove((Examination)appointment);
                _examinationReferencesRepository.Add((Examination)appointment);
                _examinationChangeRepository.Add((Examination)appointment, dateTime, resolved, AppointmentStatus.EDITED);

            }

            else if (appointment is Operation) 
            {
                _operationReferencesRepository.Add((Operation)appointment);
            }

            return resolved;

        }

        public bool CancelExamination(Appointment appointment)
        {
            if (CurrentUser is Patient && appointment.Patient.IsTrolling())
            {
                throw new PatientBlockedException("System has blocked your account !");
            }
           
            Patient patient = appointment.Patient;
            Doctor doctor = appointment.Doctor;
            Room room = appointment.Room;
            bool resolved = true;
            if (CurrentUser is Patient) resolved = appointment.IsEditable();
            if (appointment is Examination) {
                if (resolved)
                {

                    _examinationChangeRepository.RemoveByAppointmentId(appointment.ID);
                    patient.Examinations.Remove((Examination)appointment);
                    doctor.Examinations.Remove((Examination)appointment);
                    _examinationRepository.Remove((Examination)appointment);
                    _examinationReferencesRepository.Remove((Examination)appointment);
                }
                _examinationChangeRepository.Add((Examination)appointment, appointment.Date, resolved, AppointmentStatus.DELETED);
            }

            else if (appointment is Operation)
            {
                _examinationChangeRepository.RemoveByAppointmentId(appointment.ID);
                patient.Operations.Remove((Operation)appointment);
                doctor.Operations.Remove((Operation)appointment);
                _operationRepository.Remove((Operation)appointment);
                _operationReferencesRepository.Remove((Operation)appointment);

            }

            // DO NOT DELETE THIS
            if (resolved) room.Appointments.Remove(appointment);


            return resolved;
        }

        private void ValidateAppointmentData(Patient patient, Doctor doctor, DateTime dateTime)
        {
            if (CurrentUser is Patient)
            {
                if (DateTime.Compare(DateTime.Now, dateTime) > 0)
                {
                    throw new DateException("Date must be in future !");
                }
                if ((dateTime - DateTime.Now).TotalDays < 1)
                {
                    throw new DateException("Cannot schedule in next 24 hours");
                }
                if (doctor is null)
                {
                    throw new EmptyFieldException("Doctor not selected !");
                }
                if (!patient.IsAvailable(dateTime))
                {
                    throw new UserNotAvailableException("You are not available at selected time !");
                }
                if (!doctor.IsAvailable(dateTime))
                {
                    throw new UserNotAvailableException("Doctor not available at selected time !");
                }
            }

            
        }
    }
}
