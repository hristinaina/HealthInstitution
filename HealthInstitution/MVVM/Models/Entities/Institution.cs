using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.Repositories;
using System;

namespace HealthInstitution.MVVM.Models
{
    // class through which all system entities can be accessed
    // implemented using Singleton pattern
    public sealed class Institution
    {
        public PatientRepository PatientRepository;
        public DoctorRepository DoctorRepository;
        public SecretaryRepository SecretaryRepository;
        public AdminRepository AdminRepository;
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
            AdminRepository = new AdminRepository(AppSettings.Instance().GetAdminFileName());
            SecretaryRepository = new SecretaryRepository(AppSettings.Instance().GetSecretaryFileName());
            PatientRepository = new PatientRepository(AppSettings.Instance().GetPatientFileName());
            DoctorRepository = new DoctorRepository(AppSettings.Instance().GetDoctorFileName());
            // TODO: add other repositories
        }

        public bool Login(string email, string password)
        {

            if (String.IsNullOrEmpty(email) | String.IsNullOrEmpty(password))
            {
                Console.WriteLine("Niste popunili sva polja.");
                //TODO prikazati kao MessageDialog
                return false;
            }

            Patient userPat = User.FindUser(PatientRepository.GetPatients(), email, password);
            if (userPat != null)
            {
                Console.WriteLine("Uspjesno ste se ulogovali!");
                Console.WriteLine("GLAVNI MENI PACIJENTA");
                return true;
            }

            Doctor userDoc = User.FindUser(DoctorRepository.GetDoctors(), email, password);
            if (userDoc != null)
            {
                Console.WriteLine("Uspjesno ste se ulogovali!");
                Console.WriteLine("GLAVNI MENI DOKTORA");
                return true;
            }

            Secretary userSec = User.FindUser(SecretaryRepository.GetSecretaries(), email, password);
            if (userSec != null)
            {
                Console.WriteLine("Uspjesno ste se ulogovali!");
                Console.WriteLine("GLAVNI MENI SEKRETARA");
                return true;
            }


            Admin userAdm = User.FindUser(AdminRepository.GetAdministrators(), email, password);
            if (userAdm != null)
            {
                Console.WriteLine("Uspjesno ste se ulogovali!");
                Console.WriteLine("GLAVNI MENI UPRAVNIKA");
                return true;
            }

            Console.WriteLine("Ne postoji korisnik sa unesenim podacima!");
            //TODO: prikazati kao MessageDialog
            return false;
        }

        public void LoadAll()
        {
            AdminRepository.LoadFromFile();
            PatientRepository.LoadFromFile();
            DoctorRepository.LoadFromFile();
            SecretaryRepository.LoadFromFile();
            // TODO: add other repositories
        }

        public void SaveAll()
        {
            AdminRepository.SaveToFile();
            PatientRepository.SaveToFile();
            DoctorRepository.SaveToFile();
            SecretaryRepository.SaveToFile();
            // TODO: add other repositories
        }
    }
}
