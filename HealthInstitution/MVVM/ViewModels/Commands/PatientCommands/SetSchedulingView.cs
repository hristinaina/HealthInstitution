using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class SetSchedulingView : BaseCommand
    {
        private PatientSearchViewModel _viewModel;
        private NavigationStore _navigationStore;

        public SetSchedulingView(PatientSearchViewModel viewModel)
        {
            _viewModel = viewModel;
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            int doctorId = _viewModel.DoctorSelectedValue.Id;
            Doctor doctor = Institution.Instance().DoctorRepository.FindByID(doctorId);
            _navigationStore.CurrentViewModel = new PatientAppointmentViewModel(doctor);
        }
    }
}
