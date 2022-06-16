using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Repository;

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

            IEquipmentRepositoryService service = new EquipmentRepositoryService();
            _model.AllEquipment = service.GetEquipment();
            _model.FillEquipmentList();
        }
    }
}
