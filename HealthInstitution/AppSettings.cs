using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution
{
	// class that stores the names of the files that store app data
	public class AppSettings
    {
		private readonly string patientFileName;
		private readonly string doctorFileName;
		private readonly string secretaryFileName;
		private readonly string adminFileName;
		// TODO: dodati ostale atribute koji cuvaju nazive fajlova (odraditi za sve fajlove)

		public AppSettings(string patientFileName, string doctorFileName, string secretaryFileName, string adminFileName)
		{
			this.patientFileName = patientFileName;
			this.doctorFileName = doctorFileName;
			this.secretaryFileName = secretaryFileName;
			this.adminFileName = adminFileName;
			// TODO: dopuniti konstruktor ostalim atributima
		}

		public string GetAdminFileName()
		{
			return this.adminFileName;
		}

		public string GetPatientFileName()
		{
			return this.patientFileName;
		}

		public string GetDoctorFileName()
		{
			return this.doctorFileName;
		}

		public string GetSecretaryFileName()
		{
			return this.secretaryFileName;
		}
	}
}
