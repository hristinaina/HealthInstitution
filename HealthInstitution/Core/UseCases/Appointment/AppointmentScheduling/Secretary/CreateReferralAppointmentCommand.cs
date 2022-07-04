using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands
{
    public class CreateReferralAppointmentCommand : BaseCommand
    {
        private readonly Institution _institution;
        private AppointmentsViewModel _viewModel;
        private readonly SecretaryReferralService _service;

        public CreateReferralAppointmentCommand(AppointmentsViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new SecretaryReferralService();
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            DateTime datetime = _viewModel.MergeTime(_viewModel.NewAppointmentDate, _viewModel.NewAppointmentTime);

            try
            {
                _service.UseReferral(_viewModel.SelectedReferralId, datetime);
            }

            catch (PatientBlockedException e)
            {
                _viewModel.ShowMessage(e.Message, logOut: true);
                _viewModel.ShowMessage(e.Message);
                return;
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
                return;
            }

            _service.RemoveReferral(_viewModel.SelectedReferralId);
            _viewModel.ShowMessage("Appointment successfully scheduled !");

            _viewModel.FillReferralsList();
        }
    }
}
