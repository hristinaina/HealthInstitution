namespace HealthInstitution.MVVM.Models
{
	// Institution is implemented with Singleton pattern
	public sealed class Institution
	{
		private AppSettings appSettings;
		public readonly PatientRepository PatientRepository;
		public readonly DoctorRepository DoctorRepository;
		public readonly SecretaryRepository SecretaryRepository;
		public readonly AdminRepository AdminRepository;
		// TODO: dodati ostale repozitorijume

		private static Institution? s_instance = null;

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
				throw new InvalidOperationException("RepositoryFactory not created - use GetInstance(appSettings) instead");
			return s_instance;
		}

		private Institution(AppSettings appSettings)
		{
			this.appSettings = appSettings;
			this.AdminRepository = new AdminRepository(this.appSettings.GetAdminFileName());
			this.SecretaryRepository = new SecretaryRepository(this.appSettings.GetSecretaryFileName());
			this.PatientRepository = new PatientRepository(this.appSettings.GetPatientFileName());
			this.DoctorRepository = new DoctorRepository(this.appSettings.GetDoctorFileName());
			// TODO: dodati za ostale repozitorijume
		}

		public static void Login(string email, string password)
        {

            if (String.IsNullOrEmpty(email) | String.IsNullOrEmpty(password))
            {
                Console.WriteLine("Niste popunili sva polja.");
                //TODO prikazati kao MessageDialog
                return;
            }

            Patient? userPat = User.FindUser(PatientRepository.GetPatients(), email!, password!);
            if (userPat != null)
            {
                Console.WriteLine("Uspjesno ste se ulogovali!");

                Console.WriteLine("GLAVNI MENI PACIJENTA");

                return;
            }

            Doctor? userDoc = User.FindUser(DoctorRepository.GetDoctors(), email!, password!);
            if (userDoc != null)
            {
                Console.WriteLine("Uspjesno ste se ulogovali!");
                Console.WriteLine("GLAVNI MENI DOKTORA");
                return;

            }

            Secretary? userSec = User.FindUser(SecretaryRepository.GetSecretaries(), email!, password!);
            if (userSec != null)
            {
                Console.WriteLine("Uspjesno ste se ulogovali!");
                Console.WriteLine("GLAVNI MENI SEKRETARA");
                return;
            }


            Admin? userAdm = User.FindUser(AdminRepository.GetAdministrators(), email!, password!);
            if (userAdm != null)
            {
                Console.WriteLine("Uspjesno ste se ulogovali!");
                Console.WriteLine("GLAVNI MENI UPRAVNIKA");
                return;
            }

            Console.WriteLine("Ne postoji korisnik sa unesenim podacima!");
            //TODO: prikazati kao MessageDialog

        }

		public void loadAll()
		{
			this.AdminRepository.LoadFromFile();
			this.PatientRepository.LoadFromFile();
			this.DoctorRepository.LoadFromFile();
			this.SecretaryRepository.LoadFromFile();
			// TODO: dodati ostalo
		}

		public void saveAll()
		{
			this.AdminRepository.SaveToFile();
			this.PatientRepository.SaveToFile();
			this.DoctorRepository.SaveToFile();
			this.SecretaryRepository.SaveToFile();
			// TODO: dodati ostalo
		}

	}
}
