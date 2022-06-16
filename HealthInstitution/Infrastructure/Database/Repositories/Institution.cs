using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Repositories;
using System;
using System.Collections.Generic;
using HealthInstitution.Core.Services;
using HealthInstitution.Infrastructure.Database.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core
{
    // class through which all system entities can be accessed
    // implemented using Singleton pattern
    public sealed class Institution
    {
        private readonly AppConfiguration _appSettings;

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

        private readonly IMedicineRepository _medicineRepository;
        private readonly IDayOffRepository _dayOffRepository;
        private readonly ReferralRepository _referralRepository;

        private readonly IAllergenRepository _allergenRepository;
        private readonly PatientAllergenRepository _patientAllergenRepository;
        private readonly IMedicineAllergenRepository _medicineAllergenRepository;
        private readonly IPendingMedicineRepository _pendingMedicineRepository;

        private readonly IDoctorDaysOffRepository _doctorDaysOffRepository;
        private readonly PrescriptionMedicineRepository _prescriptionMedicineRepository;
        private readonly ExaminationChangeRepository _examinationChangeRepository;

        private readonly NotificationRepository _notificationRepository;
        private readonly ReviewRepository _reviewRepository;


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
            _appSettings = AppConfiguration.Instance();
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
            _reviewRepository = new ReviewRepository(_appSettings.ReviewsFileName);

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
            _reviewRepository.LoadFromFile();
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
            _reviewRepository.SaveToFile();
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
        public IMedicineRepository MedicineRepository { get => _medicineRepository; }
        public IDayOffRepository DayOffRepository { get => _dayOffRepository; }
        public ReferralRepository ReferralRepository { get => _referralRepository; }
        public IAllergenRepository AllergenRepository { get => _allergenRepository; }
        public PatientAllergenRepository PatientAllergenRepository { get => _patientAllergenRepository; }
        public IMedicineAllergenRepository MedicineAllergenRepository { get => _medicineAllergenRepository; }
        public IPendingMedicineRepository PendingMedicineRepository { get => _pendingMedicineRepository; }
        public IDoctorDaysOffRepository DoctorDaysOffRepository { get => _doctorDaysOffRepository; }
        public PrescriptionMedicineRepository PrescriptionMedicineRepository { get => _prescriptionMedicineRepository; }
        public ExaminationChangeRepository ExaminationChangeRepository { get => _examinationChangeRepository; }
        public EquipmentArrangementRepository EquipmentArragmentRepository { get => _equipmentArragmentRepository; }
        public EquipmentOrderRepository EquipmentOrderRepository { get => _equipmentOrderRepository; }
        public NotificationRepository NotificationRepository { get => _notificationRepository; }
        public ReviewRepository ReviewRepository { get => _reviewRepository; }
    }
}
