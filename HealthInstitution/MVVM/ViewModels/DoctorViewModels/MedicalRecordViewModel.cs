using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class MedicalRecordViewModel : BaseViewModel
    {
        public DoctorNavigationViewModel Navigation { get; }

        private readonly Examination _examination;
        public Examination Examination { get => _examination; }

        public string Height => _examination.Patient.Record.Height.ToString();
        public string Weight => _examination.Patient.Record.Weight.ToString();
        public string Name => _examination.Patient.FirstName.ToString() + " " +
                              _examination.Patient.LastName.ToString();
        public string Anamnesis => _examination.Anamnesis;

        public MedicalRecordViewModel(Examination examination) : this()
        {
            //Navigation = new DoctorNavigationViewModel();
            _examination = examination;
        }
        public MedicalRecordViewModel()
        {
            Navigation = new DoctorNavigationViewModel();
        }
    }
}
