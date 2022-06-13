using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services.SearchingServices;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System.Collections.Generic;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class SearchCommand : BaseCommand
    {
        private readonly BaseViewModel _viewModel;

        public SearchCommand(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (_viewModel is PatientRecordViewModel recordViewModel)
            {
                AnamnesisSearchService service = new AnamnesisSearchService(recordViewModel.Patient);
                List<Appointment> appointments = service.SearchByAnamnesis(recordViewModel.SearchKeyWord);
                recordViewModel.FillAppointmentsList(appointments);
            }
            if (_viewModel is PatientSearchViewModel searchViewModel)
            {

                DoctorsSearchService service = new DoctorsSearchService(Institution.Instance().DoctorRepository);
                Doctor search = new Doctor(searchViewModel.FirstNameKeyWord, searchViewModel.LastNameKeyWord, (Specialization)searchViewModel.SelectedSpecialization);
                List<Doctor> doctors = service.SearchForDoctor(search);
                searchViewModel.FillAllDoctorsList(doctors);
            }
        }
    }
}
