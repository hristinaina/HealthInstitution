using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DoctorExaminationViewModel : BaseViewModel
    {
        private readonly ObservableCollection<ExaminationViewModel> _examinations;

        public IEnumerable<ExaminationViewModel> Examinations => _examinations;

        public ICommand ScheduleExaminationCommand { get; }
        public ICommand StartExamination { get; }
        public ICommand MedicalRecord { get; }
        public ICommand UpdateExamination { get; }
        public ICommand DeleteExamination { get; }
        public ICommand CreateExamination { get; }
        public ICommand CancelExamination { get; }

        public DoctorExaminationViewModel()
        {
            _examinations = new ObservableCollection<ExaminationViewModel>();

            // test
            Examination exam = new Examination(1, DateTime.Now, false, false, "", new ExaminationReview(0.0, ""));
            Room room = new Room("neka soba");
            exam.Room = room;
            Patient pat = new Patient();
            pat.FirstName = "Neko";
            pat.LastName = "Nekic";
            exam.Patient = pat;

            _examinations.Add(new ExaminationViewModel(exam));
            System.Diagnostics.Debug.WriteLine(exam.Date);
            Examination exam2 = new Examination(1, DateTime.Now, false, false, "", new ExaminationReview(0.0, ""));
            exam2.Date = DateTime.Now.AddDays(5);
            Patient patt = new Patient();
            patt.FirstName = "hajd";
            patt.LastName = "bolan";
            exam2.Patient = patt;
            exam2.Room = room;
            _examinations.Add(new ExaminationViewModel(exam2));
            _examinations.Add(new ExaminationViewModel(exam2));
            System.Diagnostics.Debug.WriteLine(_examinations[0].Date);

            // test
        }
    }
}
