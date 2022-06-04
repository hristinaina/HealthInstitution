using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services.Equipments;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands { 
    class UpdateEquipmentQuantityCommand : BaseCommand
    {
        private UpdateMedicalRecordViewModel _viewModel;

        public UpdateEquipmentQuantityCommand(UpdateMedicalRecordViewModel medicalRecordViewModel)
        {
            _viewModel = medicalRecordViewModel;
        }

        public override void Execute(object parameter)
        {
            Equipment equipment = new Equipment(_viewModel.SelectedEquipment.Equipment);
            Room room = _viewModel.Examination.Room;
            try
            {
                int newQuantity = _viewModel.GetPreviousQuantityOfSelectedEquipment() - _viewModel.SpentEquipment;
                if (newQuantity < 0)
                {
                    throw new Exception();
                }

                _viewModel.SelectedEquipment.Equipment.ArrangmentByRooms[room] = newQuantity;
                EquipmentArrangementService service = new EquipmentArrangementService();
                service.UpdateEquipmentQuantityInRoom(room, _viewModel.SelectedEquipment.Equipment);

                _viewModel.UpdateQuantity(equipment, newQuantity);
                
            } catch (Exception e)
            {
                _viewModel.DialogOpen = false;
                _viewModel.ShowMessage("Input is not valid !");
            }
           
           
        }

     
    }
}
