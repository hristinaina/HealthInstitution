using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using System.Collections.ObjectModel;
using HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands;
using HealthInstitution.MVVM.Models.Services.DoctorServices;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DoctorDaysOffViewModel : BaseViewModel
    {
        public DoctorNavigationViewModel Navigation { get; }

        private Institution _institution;
        private ObservableCollection<DayOffListItemViewModel> _daysOffRequests;
        public IEnumerable<DayOffListItemViewModel> DaysOffRequests => _daysOffRequests;

        public ICommand RequestDaysOff { get; }
        
        private int _selection;
        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; };
                _selection = value;
                OnPropertyChanged(nameof(Selection));
            }
        }

        private bool _dialogOpen;
        public bool DialogOpen
        {
            get => _dialogOpen;
            set
            {
                _dialogOpen = value;
                OnPropertyChanged(nameof(DialogOpen));
            }
        }

        public DoctorDaysOffViewModel()
        {
            bool isSpecialist = true;
            Doctor doctor = (Doctor)Institution.Instance().CurrentUser;
            if (doctor.Specialization == Specialization.NONE) isSpecialist = false;
            Navigation = new DoctorNavigationViewModel(isSpecialist);

            _institution = Institution.Instance();

            // commands
            RequestDaysOff = new RequestDaysOffCommand(this);

            // initalize lists
            _daysOffRequests = new ObservableCollection<DayOffListItemViewModel>();

            // fill lists
            FindDaysOffRequests();

        }

        public void FindDaysOffRequests()
        {
            _daysOffRequests.Clear();
            DayOffService service = new DayOffService();
            List<DayOff> daysOffRequests = service.FindByDoctorID(_institution.CurrentUser.ID);
            foreach (DayOff dayOff in daysOffRequests) 
                _daysOffRequests.Add(new DayOffListItemViewModel(dayOff));
        }
    }
}
