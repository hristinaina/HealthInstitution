using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class ScheduleExaminationCommand : BaseCommand
    {
        private readonly DoctorExaminationViewModel _doctorExaminationViewModel;

        public ScheduleExaminationCommand(DoctorExaminationViewModel doctorExaminationViewModel)
        {
            _doctorExaminationViewModel = doctorExaminationViewModel;
        }

        public override void Execute(object parameter)
        {
            
        }
    }
}
