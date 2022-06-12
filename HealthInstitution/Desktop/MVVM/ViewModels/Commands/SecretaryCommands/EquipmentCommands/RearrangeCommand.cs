using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services.Equipments;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.EquipmentCommands
{
    public class RearrangeCommand : BaseCommand
    {
        private ArrangingEquipmentViewModel _model;

        public RearrangeCommand(ArrangingEquipmentViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {

            try
            {
                DateTime newArrangementStartDate = DateTime.Now.AddHours(-1);
                EquipmentService service = new EquipmentService(_model.SelectedEquipment.Equipment);
                service.Rearrange(_model.SelectedEquipment.Room, _model.NewArrangementTargetRoom, newArrangementStartDate, _model.NewArrangementQuantity);
                
                _model.DialogOpen = false;
                _model.FillEquipmentArrangementList();

            }
            catch (KeyNotFoundException e)
            {
                MessageBox.Show("This room doesn't store that kind of equipment! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
