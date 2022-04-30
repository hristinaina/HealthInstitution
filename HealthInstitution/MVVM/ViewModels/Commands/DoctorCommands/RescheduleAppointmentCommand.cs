using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class RescheduleAppointmentCommand : BaseCommand
    {

        private DoctorExaminationViewModel _viewModel;

        public RescheduleAppointmentCommand(DoctorExaminationViewModel examinationViewModel)
        {
            _viewModel = examinationViewModel;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
