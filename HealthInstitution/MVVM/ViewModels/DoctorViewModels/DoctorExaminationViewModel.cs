using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DoctorExaminationViewModel : BaseViewModel
    {
        private readonly ObservableCollection<ExaminationViewModel> _examinations;

        public IEnumerable<ExaminationViewModel> Examinations => _examinations;
        public ICommand ScheduleExaminationCommand { get; }

        public DoctorExaminationViewModel()
        {
            _examinations = new ObservableCollection<ExaminationViewModel>();
        }
    }
}
