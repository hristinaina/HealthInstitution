using HealthInstitution.Core;
using HealthInstitution.Core.Services.PatientServices;
using HealthInstitution.MVVM.ViewModels.Commands;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.Views.PatientViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class PatientRecordViewModel : BaseViewModel
    {
        private Institution _institution;
        protected Patient _patient;
        public PatientNavigationViewModel Navigation { get; }
        public Patient Patient => _patient;


        private ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments {
            get { return _appointments; }
            set { _appointments = new ObservableCollection<AppointmentListItemViewModel>(value); }
        }

        private string _searchKeyWord;
        public string SearchKeyWord {
            get { return _searchKeyWord; }
            set { _searchKeyWord = value; OnPropertyChanged(SearchKeyWord); }
        }
        public ICommand Search { get; set; }
        public ICommand Reset { get; set; }



        public PatientRecordViewModel()
        {
            Navigation = new PatientNavigationViewModel();

            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();
            PatientAppointmentsService service = new PatientAppointmentsService(_patient);
            FillAppointmentsList(service.GetPastAppointments());
            InitializeSearchParameters();

            // ..............
        }

        private void InitializeSearchParameters()
        {
            _searchKeyWord = "";
            Search = new SearchCommand(this);
            Reset = new ResetCommand(this);
        }

        public void FillAppointmentsList(List<Appointment> appointments)
        {
            _appointments.Clear();
            foreach (Appointment appointment in appointments)
            {
                _appointments.Add(new AppointmentListItemViewModel(appointment));
            }
            OnPropertyChanged(nameof(Appointments));
        }

    }
}
