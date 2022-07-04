using System;
using System.Collections.Generic;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Core.Services;
using HealthInstitution.Desktop.MVVM.Models.Services.Equipments;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.EquipmentCommands
{
    public class DynamicRearrangeCommand : BaseCommand
    {
        private ArrangingEquipmentViewModel _model;

        public DynamicRearrangeCommand(ArrangingEquipmentViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {

            try
            {
                DateTime newArrangementStartDate = DateTime.Now.AddHours(-1);
                IEquipmentRearrangementService service = new EquipmentRearrangementService();
                service.Rearrange(_model.SelectedEquipment.Room, _model.NewArrangementTargetRoom, newArrangementStartDate, _model.NewArrangementQuantity, _model.SelectedEquipment.Equipment);
               
                _model.DialogOpen = false;
                _model.FillEquipmentArrangementList();

            }
            catch (KeyNotFoundException e)
            {
                MessageBox.Show("This room doesn't store that kind of equipment! ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
