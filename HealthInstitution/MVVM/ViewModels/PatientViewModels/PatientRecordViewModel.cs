using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
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

        private readonly ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments => _appointments;


        public PatientRecordViewModel()
        {
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            Navigation = new PatientNavigationViewModel();
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();
            FillAppointmentsList();

            // ..............
        }

        public void FillAppointmentsList()
        {
            _appointments.Clear();
            foreach (Appointment appointment in _patient.GetFutureAppointments())
            {
                _appointments.Add(new AppointmentListItemViewModel(appointment));
            }
            //if (_appointments.Count != 0)
            //{
            //    Selection = 0;
            //    EnableChanges = true;
            //    OnPropertyChanged(nameof(Selection));
            //    OnPropertyChanged(nameof(EnableChanges));
            //}
            OnPropertyChanged(nameof(Appointments));
        }

    }
}
