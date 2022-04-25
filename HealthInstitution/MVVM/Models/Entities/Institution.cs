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
        private List<Patient> _patients;
        private List<Doctor> _doctors;
        private List<Secretary> _secretaries; 
        private List<Admin> _admins;
        private List<Appointment> _appointments;
        private List<Equipment> _equipment;
        private List<Operation> _operations;
        private List<Room> _rooms;
        private List<Medicine> _medicines;
        private List<DayOff> _daysOff;
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
            _admins = new List<Admin>();
            _patients = new List<Patient>();
            _doctors = new List<Doctor>();
            _secretaries = new List<Secretary>();
            _appointments = new List<Appointment>();
            _equipment = new List<Equipment>();
            _operations = new List<Operation>();
            _rooms = new List<Room>();
            _medicines = new List<Medicine>();
            _daysOff = new List<DayOff>();
            // TODO: add other repositories
        }

        public void LoadAll()
        {
            _admins = FileService.Deserialize<Admin>(AppSettings.Instance().GetAdminFileName());
            _patients = FileService.Deserialize<Patient>(AppSettings.Instance().GetPatientFileName());
            _doctors = FileService.Deserialize<Doctor>(AppSettings.Instance().GetDoctorFileName());
            _secretaries = FileService.Deserialize<Secretary>(AppSettings.Instance().GetSecretaryFileName());
            _appointments = FileService.Deserialize<Appointment>(AppSettings.Instance().GetAdminFileName());
            _equipment = FileService.Deserialize<Equipment>(AppSettings.Instance().GetEquipmentFileName());
            _operations = FileService.Deserialize<Operation>(AppSettings.Instance().GetOperationFileName());
            _rooms = FileService.Deserialize<Room>(AppSettings.Instance().GetRoomFileName());
            _medicines = FileService.Deserialize<Medicine>(AppSettings.Instance().GetMedicineFileName());
            _daysOff = FileService.Deserialize<DayOff>(AppSettings.Instance().GetDayOffFileName());
            // TODO: add other repositories
        }

        public void SaveAll()
        {
            FileService.Serialize<Admin>(AppSettings.Instance().GetAdminFileName(), _admins);
            FileService.Serialize<Patient>(AppSettings.Instance().GetPatientFileName(), _patients);
            FileService.Serialize<Doctor>(AppSettings.Instance().GetDoctorFileName(), _doctors);
            FileService.Serialize<Secretary>(AppSettings.Instance().GetSecretaryFileName(), _secretaries);
            FileService.Serialize<Appointment>(AppSettings.Instance().GetAppointmentFileName(), _appointments);
            FileService.Serialize<Equipment>(AppSettings.Instance().GetEquipmentFileName(), _equipment);
            FileService.Serialize<Operation>(AppSettings.Instance().GetOperationFileName(), _operations);
            FileService.Serialize<Room>(AppSettings.Instance().GetOperationFileName(), _rooms);
            FileService.Serialize<Medicine>(AppSettings.Instance().GetMedicineFileName(), _medicines);
            FileService.Serialize<DayOff>(AppSettings.Instance().GetDayOffFileName(), _daysOff);
            // TODO: add other repositories
        }

        public List<Patient> GetPatients()
        {
            return _patients;
        }
        public List<Admin> GetAdmins()
        {
            return _admins;
        }
        public List<Doctor> GetDoctors()
        {
            return _doctors;
        }
        public List<Secretary> GetSecretaries()
        {
            return _secretaries;
        }
        public List<Appointment> GetAppointments()
        {
            return _appointments;
        }
        public List<Equipment> GetEquipment()
        {
            return _equipment;
        }
        public List<Operation> GetOperations()
        {
            return _operations;
        }
        public List<Room> GetRooms()
        {
            return _rooms;
        }
        public List<Medicine> GetMedicines()
        {
            return _medicines;
        }
        public List<DayOff> GetDayOff()
        {
            return _daysOff;
        }

    }
}
