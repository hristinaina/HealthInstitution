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
using HealthInstitution.MVVM.Models.Services;

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
        private readonly RenovationRepository _renovationRepository;
        private readonly RoomRenovationRepository _roomRenovationRepository;
        private readonly EquipmentOrderRepository _equipmentOrderRepository;

        private readonly MedicineRepository _medicineRepository;
        private readonly DayOffRepository _dayOffRepository;
        private readonly ReferralRepository _referralRepository;

        private readonly AllergenRepository _allergenRepository;
        private readonly PatientAllergenRepository _patientAllergenRepository;
        private readonly MedicineAllergenRepository _medicineAllergenRepository;
        private readonly PendingMedicineRepository _pendingMedicineRepository;

        private readonly DoctorDaysOffRepository _doctorDaysOffRepository;
        private readonly PrescriptionMedicineRepository _prescriptionMedicineRepository;
        private readonly ExaminationChangeRepository _examinationChangeRepository;

        private readonly NotificationRepository _notificationRepository;


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

            _roomRepository = new RoomRepository(_appSettings.RoomsFileName, _appSettings.FutureRoomsFileName, _appSettings.DeletedRoomsFileName);
            _equipmentRepository = new EquipmentRepository(_appSettings.EquipmentFileName);
            _equipmentArragmentRepository = new EquipmentArrangementRepository(_appSettings.EquipmentArrangementFileName);
            _renovationRepository = new RenovationRepository(_appSettings.RenovationFileName);
            _roomRenovationRepository = new RoomRenovationRepository(_appSettings.RoomRenovationFileName);
            _equipmentOrderRepository = new EquipmentOrderRepository(_appSettings.EquipmentOrderFileName);

            _dayOffRepository = new DayOffRepository(_appSettings.DaysOffFileName);
            _referralRepository = new ReferralRepository(_appSettings.RefferalsFileName);

            _equipmentArragmentRepository = new EquipmentArrangementRepository(_appSettings.EquipmentArrangementFileName);
            _medicineRepository = new MedicineRepository(_appSettings.MedicinesFileName);
            _allergenRepository = new AllergenRepository(_appSettings.AllergensFileName);
            _patientAllergenRepository = new PatientAllergenRepository(_appSettings.PatientAllergensFileName);
            _medicineAllergenRepository = new MedicineAllergenRepository(_appSettings.MedicineAllergensFileName);
            _pendingMedicineRepository = new PendingMedicineRepository(_appSettings.PendingMedicinesFileName);

            _doctorDaysOffRepository = new DoctorDaysOffRepository(_appSettings.DoctorDaysOffFileName);
            _prescriptionMedicineRepository = new PrescriptionMedicineRepository(_appSettings.PrescriptionMedicineFileName);
            _examinationChangeRepository = new ExaminationChangeRepository(_appSettings.ExaminationChangeFileName);

            _notificationRepository = new NotificationRepository(_appSettings.PatientNotificationsFileName);

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
            _renovationRepository.LoadFromFile();
            _roomRenovationRepository.LoadFromFile();
            _medicineRepository.LoadFromFile();
            _referralRepository.LoadFromFile();
            _allergenRepository.LoadFromFile();
            _patientAllergenRepository.LoadFromFile();
            _medicineAllergenRepository.LoadFromFile();
            _pendingMedicineRepository.LoadFromFile();
            _doctorDaysOffRepository.LoadFromFile();
            _prescriptionMedicineRepository.LoadFromFile();
            _prescriptionRepository.LoadFromFile();
            _examinationChangeRepository.LoadFromFile();
            _equipmentOrderRepository.LoadFromFile();
            _notificationRepository.LoadFromFile();
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
            _renovationRepository.SaveToFile();
            _roomRenovationRepository.SaveToFile();
            _referralRepository.SaveToFile();
            _medicineRepository.SaveToFile();
            _allergenRepository.SaveToFile();
            _patientAllergenRepository.SaveToFile();
            _medicineAllergenRepository.SaveToFile();
            _pendingMedicineRepository.SaveToFile();
            _doctorDaysOffRepository.SaveToFile();
            _prescriptionMedicineRepository.SaveToFile();
            _prescriptionRepository.SaveToFile();
            _examinationChangeRepository.SaveToFile();
            _equipmentOrderRepository.SaveToFile();
            _notificationRepository.SaveToFile();
        }

        private static void ConnectReferences()
        {
            ReferencesService.ConnectExaminationReferences();
            ReferencesService.ConnectOperationReferences();
            ReferencesService.FillMedicalRecord();
            ReferencesService.ConnectMedicineAllergens();
            ReferencesService.ConnectDoctorDaysOff();
            ReferencesService.ConnectPrescriptionRepository();
            ReferencesService.ConnectExaminationChanges();
            ReferencesService.ArrangeEquipment();
            ReferencesService.ConnectRenovations();
            ReferencesService.ConnectPendingMedicineAllergens();
            ReferencesService.ConnectPatientNotifications();
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
        public RenovationRepository RenovationRepository { get => _renovationRepository; }
        public RoomRenovationRepository RoomRenovationRepository { get => _roomRenovationRepository; }
        public MedicineRepository MedicineRepository { get => _medicineRepository; }
        public DayOffRepository DayOffRepository { get => _dayOffRepository; }
        public ReferralRepository ReferralRepository { get => _referralRepository; }
        public AllergenRepository AllergenRepository { get => _allergenRepository; }
        public PatientAllergenRepository PatientAllergenRepository { get => _patientAllergenRepository; }
        public MedicineAllergenRepository MedicineAllergenRepository { get => _medicineAllergenRepository; }
        public PendingMedicineRepository PendingMedicineRepository { get => _pendingMedicineRepository; }
        public DoctorDaysOffRepository DoctorDaysOffRepository { get => _doctorDaysOffRepository; }
        public PrescriptionMedicineRepository PrescriptionMedicineRepository { get => _prescriptionMedicineRepository; }
        public ExaminationChangeRepository ExaminationChangeRepository { get => _examinationChangeRepository; }
        public EquipmentArrangementRepository EquipmentArragmentRepository { get => _equipmentArragmentRepository; }
        public EquipmentOrderRepository EquipmentOrderRepository { get => _equipmentOrderRepository; }
        public NotificationRepository NotificationRepository { get => _notificationRepository; }

        public bool CreateAppointment(Doctor doctor, Patient patient, DateTime dateTime, string type, int duration = 15, bool validation = true)
        {
            CheckTrolling();
            DoctorService doctorService = new DoctorService(doctor);
            if (CurrentUser is Patient && patient.IsTrolling())
            {
                throw new PatientBlockedException("System has blocked your account !");
            }
            if (CurrentUser is Secretary)
            {
                if (!doctorService.IsAvailable(dateTime, duration))
                {
                    return false;
                }
                if (!patient.IsAvailable(dateTime, duration))
                {
                    return false;
                }
            }
            ValidateAppointmentData(patient, doctor, dateTime, validation, duration);

            int appointmentId = 0;

            if (type == nameof(Examination))
            {

                appointmentId = _examinationRepository.NewId();
                //int prescriptionId = _prescriptionRepository.GetNewId();
                
                Examination examination = new Examination(appointmentId, doctor, patient, dateTime,
                                          new List<Prescription>());
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


        public bool RescheduleExamination(Appointment appointment, DateTime dateTime, bool validation = true)
        {
            CheckTrolling();

            ValidateAppointmentData(appointment.Patient, appointment.Doctor, dateTime, validation);

            _roomRepository.FindAvailableRoom(appointment, dateTime);
            bool resolved = true;
            if (CurrentUser is Patient) {
                resolved = appointment.IsEditable();
            }
            if (resolved)
            {
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
                _operationReferencesRepository.Remove((Operation)appointment);
                _operationReferencesRepository.Add((Operation)appointment);
            }

            return resolved;

        }

        public bool CancelExamination(Appointment appointment)
        {
            CheckTrolling();
           
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

            if (resolved) room.Appointments.Remove(appointment);
            return resolved;
        }

        public void ValidateAppointmentData(Patient patient, Doctor doctor, DateTime dateTime, bool validation, int duration=15)
        {
            if (CurrentUser is Patient || CurrentUser is Secretary || CurrentUser is Doctor)
            {
                DoctorService doctorService = new DoctorService(doctor);
                if (DateTime.Compare(DateTime.Now, dateTime) > 0 && validation)
                {
                    throw new DateException("Date must be in future !");
                }
                if (CurrentUser is not Doctor)
                {
                    if ((dateTime - DateTime.Now).TotalDays < 1 && validation)
                    {
                        throw new DateException("Cannot schedule in next 24 hours");
                    }
                }
                if (doctor is null)
                {
                    throw new EmptyFieldException("Doctor not selected !");
                }
                if (patient is null)
                {
                    throw new EmptyFieldException("Patient not selected !");
                }
                if (!patient.IsAvailable(dateTime, duration))
                {
                    throw new UserNotAvailableException("Patient not available at selected time !");
                }
                if (!doctorService.IsAvailable(dateTime, duration))
                {
                    throw new UserNotAvailableException("Doctor not available at selected time !");
                }
            }       
        }

        public void CheckTrolling()
        {
            if (CurrentUser is Patient)
            {
                Patient patient = (Patient)CurrentUser;
                if (patient.IsTrolling())
                throw new PatientBlockedException("System has blocked your account !");
            }
        }
    }
}
