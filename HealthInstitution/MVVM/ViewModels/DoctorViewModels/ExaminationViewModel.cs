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
        public Examination Examination { get => _examination; }

        public string Date => _examination.Date.ToString("dd/MM/yyyy");
        public string Time => _examination.Date.ToString("HH:MM");
        public Room Room => _examination.Room;
        public Patient Patient => _examination.Patient;

        public ExaminationViewModel(Examination examination)
        {
            _examination = examination;
        }
    }
}
