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
        private string _examinationFileName;
        private string _perscriptionFileName;
        private string _operationFileName;
        private string _examinationReferenceFileName;
        private string _operationReferenceFileName;

        private string _equipmentFileName;
        private string _roomFileName;
        private string _equipmentArragmentFileName;
        private string _medicineFileName;
        private string _dayOffFileName;
        private string _refferalFileName;
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

        public void AddFilePaths(string patientFileName, string doctorFileName, string secretaryFileName, string adminFileName,
                                 string examinationFileName, string operationFileName, string examinationReferenceFileName, string operationReferenceFileName, string equipmentFileName,
                                 string roomFileName, string equipmentArragmentFileName, string medicineFileName, string dayOffFileName, string perscriptionFileName, string refferalFileName)
        {
            _patientFileName = patientFileName;
            _doctorFileName = doctorFileName;
            _secretaryFileName = secretaryFileName;
            _adminFileName = adminFileName;

            _examinationFileName = examinationFileName;
            _perscriptionFileName = perscriptionFileName;
            _operationFileName = operationFileName;
            _examinationReferenceFileName = examinationReferenceFileName;
            _operationReferenceFileName = examinationReferenceFileName;

            _roomFileName = roomFileName;
            _equipmentFileName = equipmentFileName;
            _equipmentArragmentFileName = equipmentArragmentFileName;

            _medicineFileName = medicineFileName;

            _dayOffFileName = dayOffFileName;

            _refferalFileName = refferalFileName;

            // TODO add the rest of the class attributes
        }

        public string GetAdminFileName()
        {
            return _adminFileName;
        }

        public string GetPatientFileName()
        {
            return _patientFileName;
        }

        public string GetDoctorFileName()
        {
            return _doctorFileName;
        }

        public string GetSecretaryFileName()
        {
            return _secretaryFileName;
        }

        internal string GetEquipmentArragmentFileName()
        {
            return this._equipmentArragmentFileName;
        }

        public string GetAppointmentFileName()
        {
            return _examinationFileName;
        }

        public string GetExationFileName() {
            return _examinationFileName;
        }

        public string GetPerscriptionFileName()
        {
            return _perscriptionFileName;
        }
        public string GetOperationFileName()
        {
            return _operationFileName;
        }

        public string GetExaminationReferenceFileName()
        {
            return _examinationReferenceFileName;
        }
        public string GetOperationReferenceFileName()
        {
            return _operationReferenceFileName;
        }
        public string GetEquipmentFileName()
        {
            return _equipmentFileName;
        }
        public string GetRoomFileName()
        {
            return _roomFileName;
        }
        public string GetMedicineFileName()
        {
            return _medicineFileName;
        }
        public string GetDayOffFileName()
        {
            return _dayOffFileName;
        }

        public string GetRefferalFileName()
        {
            return _refferalFileName;
        }
    }
}
