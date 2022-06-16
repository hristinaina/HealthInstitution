using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using HealthInstitution.Core.Services.Equipments;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.EquipmentCommands
{
    class SearchCommand : BaseCommand
    {
        private AdminEquipmentViewModel _model;
        private SearchEquipmentService _search;

        public SearchCommand(AdminEquipmentViewModel model)
        {
            _model = model;
            _search = new SearchEquipmentService();
        }

        public override void Execute(object parameter)
        {
            
            try 
            {
                _model.FilteredEquipment = _search.Search(_model.SearchPhrase);

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
