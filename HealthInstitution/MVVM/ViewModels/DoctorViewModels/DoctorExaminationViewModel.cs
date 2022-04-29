using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HealthInstitution.MVVM.Models;

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

        public DoctorExaminationViewModel(ExaminationRepository examinationRepository)
        {
            _examinations = new ObservableCollection<ExaminationViewModel>();

            // add here test examples  
        }
    }
}
