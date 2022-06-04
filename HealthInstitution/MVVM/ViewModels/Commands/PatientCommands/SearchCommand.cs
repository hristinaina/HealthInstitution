using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.Models.Services.SearchingServices;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class SearchCommand : BaseCommand
    {
        private BaseViewModel _viewModel;

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
                searchViewModel.FillDoctorsList(doctors);
            }
        }
    }
}
