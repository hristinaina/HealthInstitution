using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Services;
using System.Collections.Generic;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class SearchCommand : BaseCommand
    {
        private readonly BaseViewModel _viewModel;
        IAnamnesisSearch _anamnesisSearch;
        IDoctorSearch _doctorSearch;

        public SearchCommand(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (_viewModel is PatientRecordViewModel recordViewModel)
            {
                _anamnesisSearch = new AnamnesisSearchService(recordViewModel.Patient);
                List<Appointment> appointments = _anamnesisSearch.SearchByAnamnesis(recordViewModel.SearchKeyWord);
                recordViewModel.FillAppointmentsList(appointments);
            }
            if (_viewModel is PatientSearchViewModel searchViewModel)
            {

                DoctorsSearchService _doctorSearch = new DoctorsSearchService(Institution.Instance().DoctorRepository);
                Doctor search = new Doctor(searchViewModel.FirstNameKeyWord, searchViewModel.LastNameKeyWord, (Specialization)searchViewModel.SelectedSpecialization);
                List<Doctor> doctors = _doctorSearch.SearchForDoctor(search);
                searchViewModel.FillAllDoctorsList(doctors);
            }
        }
    }
}
