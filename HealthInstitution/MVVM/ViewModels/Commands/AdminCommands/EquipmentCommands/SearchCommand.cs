using HealthInstitution.Commands;
using HealthInstitution.Exceptions.AdminExceptions;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.MVVM.Models.Services.Equipments;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.EquipmentCommands
{
    class SearchCommand : BaseCommand
    {
        private AdminEquipmentViewModel _model;

        public SearchCommand(AdminEquipmentViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            
            try 
            {
                //_model.FilteredEquipment = Institution.Instance().EquipmentRepository.Search(_model.SearchPhrase);
                SearchEquipmentService service = new SearchEquipmentService();
                _model.FilteredEquipment = service.Search(_model.SearchPhrase);

                _model.FilterEquipmentList();
            } catch (EmptySearchPhraseException e)
            {
                _model.ShowMessage(e.Message);
            } catch (Exception e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
