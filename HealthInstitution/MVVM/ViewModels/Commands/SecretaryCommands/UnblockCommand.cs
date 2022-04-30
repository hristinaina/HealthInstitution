using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class UnblockCommand : BaseCommand
    {
        private readonly Institution _institution;
        private BlockedPatientListViewModel _viewModel;

        public UnblockCommand(BlockedPatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            Patient patient = Institution.Instance().PatientRepository.FindByID(_viewModel.SelectedPatientId);
            patient.UnblockPatient();
            _viewModel.FillPatientList();
        }
    }
}
