using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution
{
	// class that stores the names of the files that store app data
	// implemented using Singleton pattern
	public sealed class AppSettings
    {
		private string _patientFileName;
		private string _doctorFileName;
		private string _secretaryFileName;
		private string _adminFileName;
		// TODO add the rest of the class attributes that will store the name of the corresponding files

		private AppSettings() { }

		private static AppSettings s_instance = null;

		public static AppSettings Instance()
		{
			if (s_instance == null)
			{
				s_instance = new AppSettings();
			}
			return s_instance;
		}

		public void AddFilePaths(string patientFileName, string doctorFileName, string secretaryFileName, string adminFileName)
		{
			this._patientFileName = patientFileName;
			this._doctorFileName = doctorFileName;
			this._secretaryFileName = secretaryFileName;
			this._adminFileName = adminFileName;
			// TODO add the rest of the class attributes
		}

		public string GetAdminFileName()
		{
			return this._adminFileName;
		}

		public string GetPatientFileName()
		{
			return this._patientFileName;
		}

		public string GetDoctorFileName()
		{
			return this._doctorFileName;
		}

		public string GetSecretaryFileName()
		{
			return this._secretaryFileName;
		}
	}
}
