using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.Repositories;
using System;

namespace HealthInstitution.MVVM.Models
{
    // Institution is implemented with Singleton pattern
    public sealed class Institution
    {
        private AppSettings appSettings;
        public PatientRepository PatientRepository;
        public DoctorRepository DoctorRepository;
        public SecretaryRepository SecretaryRepository;
        public AdminRepository AdminRepository;
        // TODO: dodati ostale repozitorijume

        private static Institution s_instance = null;

        // used in Main>Program to create an instance (only one) of RepositoryFactory class
        public static Institution Instance(AppSettings appSettings)
        {
            if (s_instance == null)
            {
                s_instance = new Institution(appSettings);
            }
            return s_instance;
        }

        // used anywhere else to get that instance
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
        }

        private Institution(AppSettings appSettings)
        {
            // TODO: dodati za ostale repozitorijume
            this.appSettings = appSettings;
            createRepositories();

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

        public void loadAll()
        {
            AdminRepository.LoadFromFile();
            PatientRepository.LoadFromFile();
            DoctorRepository.LoadFromFile();
            SecretaryRepository.LoadFromFile();
            // TODO: dodati ostalo
        }

        public void saveAll()
        {
            AdminRepository.SaveToFile();
            PatientRepository.SaveToFile();
            DoctorRepository.SaveToFile();
            SecretaryRepository.SaveToFile();
            // TODO: dodati ostalo
        }

        private void createRepositories()
        {

            AdminRepository = new AdminRepository(this.appSettings.GetAdminFileName());
            SecretaryRepository = new SecretaryRepository(this.appSettings.GetSecretaryFileName());
            PatientRepository = new PatientRepository(this.appSettings.GetPatientFileName());
            DoctorRepository = new DoctorRepository(this.appSettings.GetDoctorFileName());
        }

    }
}
