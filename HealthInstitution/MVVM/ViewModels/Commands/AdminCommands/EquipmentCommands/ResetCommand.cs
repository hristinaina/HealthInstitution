using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.EquipmentCommands
{
    class ResetCommand : BaseCommand
    {
        private AdminEquipmentViewModel _model;

        public ResetCommand(AdminEquipmentViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            _model.SearchPhrase = null;
            _model.AllEquipment = Institution.Instance().EquipmentRepository.Equipment;
            _model.FillEquipmentList();
        }
    }
}
