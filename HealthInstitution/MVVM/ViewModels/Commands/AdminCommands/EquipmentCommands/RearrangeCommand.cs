using HealthInstitution.Commands;
using HealthInstitution.Exceptions;
using HealthInstitution.Exceptions.AdminExceptions;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.EquipmentCommands
{
    class RearrangeCommand : BaseCommand
    {
        private AdminEquipmentViewModel _model;

        public RearrangeCommand(AdminEquipmentViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {

            try
            {
                DateTime newArrangementStartDate = _model.ParseDate(_model.NewArrangementStartDate);

                _model.SelectedEquipment.Equipment.Rearrange(_model.SelectedEquipment.Room, _model.NewArrangemenTargetRoom, newArrangementStartDate, _model.NewArrangementQuantity);

                _model.DialogOpen = false;
            } catch (RearrangeTargetRoomNullException e)
            {
                _model.ShowMessage(e.Message);
            }
            catch (ZeroQuantityException e)
            {
                _model.ShowMessage(e.Message);
            }
            catch (DateException e)
            {
                _model.ShowMessage(e.Message);
            }
            catch (NotEnoughEquipmentException e)
            {
                _model.ShowMessage(e.Message);
            }
            catch (Exception e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
