using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    public class ExaminationItemViewModel : BaseViewModel
    {
        private readonly Examination _examination;
        public Examination Examination { get => _examination; }

        public string Date => _examination.Date.ToString("dd/MM/yyyy");
        public string Time => _examination.Date.ToString("HH:MM");
        public Room Room => _examination.Room;
        public Patient Patient => _examination.Patient;

        public ExaminationItemViewModel(Examination examination)
        {
            _examination = examination;
        }
    }
}
