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

        private string _patientsFileName;
        private string _doctorsFileName;
        private string _daysOffFileName;
        private string _secretariesFileName;
        private string _adminsFileName;
        private string _examinationsFileName;
        private string _operationsFileName;
        private string _examinationsReferencesFileName;
        private string _operationsReferencesFileName;
        private string _perscriptionsFileName;
        private string _refferalFileName;
        private string _equipmentFileName;
        private string _equipmentArrangementFileName;
        private string _roomsFileName;
        private string _medicinesFileName;
        private string _equipmentOrderFileName;
        private string _allergensFileName;
        private string _patientAllergensFileName;
        private string _medicineAllergensFileName;


        public string PatientsFileName { get => _patientsFileName; set => _patientsFileName = value; }
        public string DoctorsFileName { get => _doctorsFileName; set => _doctorsFileName = value; }
        public string DaysOffFileName { get => _daysOffFileName; set => _daysOffFileName = value; }
        public string SecretariesFileName { get => _secretariesFileName; set => _secretariesFileName = value; }
        public string AdminsFileName { get => _adminsFileName; set => _adminsFileName = value; }
        public string ExaminationsFileName { get => _examinationsFileName; set => _examinationsFileName = value; }
        public string OperationsFileName { get => _operationsFileName; set => _operationsFileName = value; }
        public string ExaminationReferencesFileName { get => _examinationsReferencesFileName; set => _examinationsReferencesFileName = value; }
        public string OperationsReferencesFileName { get => _operationsReferencesFileName; set => _operationsReferencesFileName = value; }
        public string PerscriptionsFileName { get => _perscriptionsFileName; set => _perscriptionsFileName = value; }
        public string RefferalsFileName { get => _refferalFileName; set => _refferalFileName = value; }
        public string EquipmentFileName { get => _equipmentFileName; set => _equipmentFileName = value; }
        public string EquipmentArrangementFileName { get => _equipmentArrangementFileName; set => _equipmentFileName = value; }
        public string RoomsFileName { get => _roomsFileName; set => _roomsFileName = value; }
        public string MedicinesFileName { get => _medicinesFileName; set => _medicinesFileName = value; }
        public string AllergensFileName { get => _allergensFileName; set => _allergensFileName = value; }
        public string PatientAllergensFileName { get => _patientAllergensFileName; set => _patientAllergensFileName = value; }
        public string MedicineAllergensFileName { get => _medicineAllergensFileName; set => _medicineAllergensFileName = value; }
        public string EquipmentOrderFileName { get => _equipmentOrderFileName; set => _equipmentOrderFileName = value; }
        // TODO add the rest of the class attributes that will store the name of the corresponding files


        private AppSettings() { }

        private static AppSettings s_instance = null;

        public static AppSettings Instance()
        {
            if (s_instance is null)
            {
                s_instance = new AppSettings();
            }
            return s_instance;
        }
        public static void SetInstance(AppSettings instance)
        {
            if (s_instance is null)
            {
                s_instance = instance;
            }
        }

        /*public void AddFilePaths(string patientFileName, string doctorFileName, string secretaryFileName, string adminFileName,
                                 string examinationFileName, string operationFileName, string examinationReferenceFileName, string operationReferenceFileName, string equipmentFileName,
                                 string roomFileName, string equipmentArragmentFileName, string medicineFileName, string dayOffFileName, string perscriptionFileName, string refferalFileName,
                                 string allergenFileName, string medicineAllergenFileName, string patientAllergenFileName)
        {
            _patientsFileName = patientFileName;
            _doctorsFileName = doctorFileName;
            _secretariesFileName = secretaryFileName;
            _adminsFileName = adminFileName;

            _examinationsFileName = examinationFileName;
            _perscriptionsFileName = perscriptionFileName;
            _operationsFileName = operationFileName;
            _examinationsReferencesFileName = examinationReferenceFileName;
            _operationsReferencesFileName = examinationReferenceFileName;

            _allergensFileName = allergenFileName;
            _medicineAllergensFileName = medicineAllergenFileName;
            _patientAllergensFileName = patientAllergenFileName;

            _roomsFileName = roomFileName;
            _equipmentFileName = equipmentFileName;
            _equipmentArrangementFileName = equipmentArragmentFileName;

            _medicinesFileName = medicineFileName;

            _daysOffFileName = dayOffFileName;

            _refferalFileName = refferalFileName;

            // TODO add the rest of the class attributes
        }*/

    }
}
