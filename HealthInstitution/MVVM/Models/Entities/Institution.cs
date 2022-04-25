using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.Repositories;
using System;
using System.Collections.Generic;
using Baza;

namespace HealthInstitution.MVVM.Models
{
    // class through which all system entities can be accessed
    // implemented using Singleton pattern
    public sealed class Institution
    {
        public List<Patient> Patients;
        public List<Doctor> Doctors;
        public List<Secretary> Secretaries;
        public List<Admin> Admins;
        public List<Appointment> Appointments;
        public List<Equipment> Equipments;
        public List<Operation> Operations;
        public List<Room> Rooms;
        public List<Medicine> Medicines;
        public List<DayOff> DaysOff;
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
/*          AdminRepository = new AdminRepository(AppSettings.Instance().GetAdminFileName());
            SecretaryRepository = new SecretaryRepository(AppSettings.Instance().GetSecretaryFileName());
            PatientRepository = new PatientRepository(AppSettings.Instance().GetPatientFileName());
            DoctorRepository = new DoctorRepository(AppSettings.Instance().GetDoctorFileName());*/
            Admins = new List<Admin>();
            Patients = new List<Patient>();
            Doctors = new List<Doctor>();
            Secretaries = new List<Secretary>();
            Appointments = new List<Appointment>();
            Equipments = new List<Equipment>();
            Operations = new List<Operation>();
            Rooms = new List<Room>();
            Medicines = new List<Medicine>();
            DaysOff = new List<DayOff>();
            // TODO: add other repositories
        }

        public void LoadAll()
        {
            Admins = FileService.Deserialize<Admin>("");
            Patients = FileService.Deserialize<Patient>("");
            Doctors = FileService.Deserialize<Doctor>("");
            Secretaries = FileService.Deserialize<Secretary>("");
            Appointments = FileService.Deserialize<Appointment>("");
            Equipments = FileService.Deserialize<Equipment>("");
            Operations = FileService.Deserialize<Operation>("");
            Rooms = FileService.Deserialize<Room>("");
            Medicines = FileService.Deserialize<Medicine>("");
            DaysOff = FileService.Deserialize<DayOff>("");
            // TODO: add other repositories
        }

        public void SaveAll()
        {
            FileService.Serialize<Admin>("", Admins);
            FileService.Serialize<Patient>("", Patients);
            FileService.Serialize<Doctor>("", Doctors);
            FileService.Serialize<Secretary>("", Secretaries);
            FileService.Serialize<Appointment>("", Appointments);
            FileService.Serialize<Equipment>("", Equipments);
            FileService.Serialize<Operation>("", Operations);
            FileService.Serialize<Room>("", Rooms);
            FileService.Serialize<Medicine>("", Medicines);
            FileService.Serialize<DayOff>("", DaysOff);
            // TODO: add other repositories
        }
    }
}
