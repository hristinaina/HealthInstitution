using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class RequestDaysOffCommand : BaseCommand
    {
        private DoctorDaysOffViewModel _viewModel;

        public RequestDaysOffCommand(DoctorDaysOffViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            try
            {
                int id = Institution.Instance().DayOffRepository.FindNewID();
                DateTime startDate = Convert.ToDateTime(_viewModel.StartDate);
                DateTime endDate = Convert.ToDateTime(_viewModel.EndDate);
                Doctor doctor = (Doctor)Institution.Instance().CurrentUser;
                DayOff dayOff = new DayOff(id, startDate, endDate, _viewModel.IsEmegency,
                                           _viewModel.Reason, doctor);
                DayOffService service = new DayOffService();
                if (_viewModel.IsEmegency is true) dayOff.State = State.ACCEPTED;
                bool isAppplied = service.ApplyForDaysOff(dayOff, doctor);

                if (!isAppplied) _viewModel.ShowMessage("Request is rejected automatically !");
                else _viewModel.ShowMessage("Request is successfully sent! ");
               
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }

            _viewModel.FindDaysOffRequests();
            
        }
    }
}
