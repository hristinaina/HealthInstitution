using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    public class ExaminationViewModel : BaseViewModel
    {
        private readonly Examination _examination;

        public DateTime DateTime => _examination.Date;
        public string Room => _examination.Room.ID.ToString();
        public string Patient => _examination.Patient.FirstName + _examination.Patient.LastName;

        public ExaminationViewModel(Examination examination)
        {
            _examination = examination;
        }
    }
}
