using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class ScheduleExaminationCommand : BaseCommand
    {
        private readonly ExaminationRepository _examination;

        public override void Execute(object parameter)
        {
            
        }
    }
}
