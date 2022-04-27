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
        private string _operationFileName;
        private string _examinationReferenceFileName;
        private string _operationReferenceFileName;

        private string _equipmentFileName;
        private string _roomFileName;
        private string _medicineFileName;
        private string _dayOffFileName;
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
                                 string roomFileName, string medicineFileName, string dayOffFileName)
        {
            this._patientFileName = patientFileName;
            this._doctorFileName = doctorFileName;
            this._secretaryFileName = secretaryFileName;
            this._adminFileName = adminFileName;

            this._examinationFileName = examinationFileName;
            this._operationFileName = operationFileName;
            _examinationReferenceFileName = examinationReferenceFileName;
            _operationReferenceFileName = examinationReferenceFileName;

            this._roomFileName = roomFileName;
            this._equipmentFileName = equipmentFileName;

            this._medicineFileName = medicineFileName;

            this._dayOffFileName = dayOffFileName;

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
        public string GetAppointmentFileName()
        {
            return this._examinationFileName;
        }

        public string GetExationFileName() {
            return _examinationFileName;
        }
        public string GetOperationFileName()
        {
            return this._operationFileName;
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
            return this._equipmentFileName;
        }
        public string GetRoomFileName()
        {
            return this._roomFileName;
        }
        public string GetMedicineFileName()
        {
            return this._medicineFileName;
        }
        public string GetDayOffFileName()
        {
            return this._dayOffFileName;
        }
    }
}
