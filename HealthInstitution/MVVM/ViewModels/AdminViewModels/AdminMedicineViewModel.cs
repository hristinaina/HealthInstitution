using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.MedicineCommands;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class AdminMedicineViewModel : BaseViewModel
    {
        private readonly Admin _admin;
        private Institution _institution;
        private MedicineListItemViewModel _selectedMedicine;

        public MedicineListItemViewModel SelectedMedicine { get => _selectedMedicine; }


        private bool _enableChanges;
        private int _selection;
        public bool EnableChanges
        {
            get => _enableChanges;
            set
            {
                _enableChanges = value;
                OnPropertyChanged(nameof(EnableChanges));
            }
        }

        private bool _dialogOpen;
        public bool DialogOpen
        {
            get => _dialogOpen;
            set
            {
                _dialogOpen = value;
                OnPropertyChanged(nameof(DialogOpen));
            }
        }

        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { EnableChanges = false; return; };
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedMedicine = _medicine.ElementAt(_selection);
                SelectedID = _selectedMedicine.ID;
                OnPropertyChanged(nameof(SelectedID));
                SelectedName = _selectedMedicine.Name;
                OnPropertyChanged(nameof(SelectedName));
                SelectedState = _selectedMedicine.State;
                OnPropertyChanged(nameof(SelectedState));
                SelectedStateIndex = (int)_selectedMedicine.Medicine.State;
                OnPropertyChanged(nameof(SelectedStateIndex));
                SelectedIngredients = _selectedMedicine.MedicineIngredients;
                OnPropertyChanged(nameof(SelectedIngredients));
            }
        }


        private IngredientListItemViewModel _newMedicineSelection;

        public IngredientListItemViewModel NewMedicineSelection
        {
            get => _newMedicineSelection;
            set
            {
                if (value is null) { EnableChanges = false; return; };
                _newMedicineSelection = value;
                if (_newMedicineSelection.Selected)
                {
                    _newMedicineSelection.Selected = false;
                }
                else
                {
                    _newMedicineSelection.Selected = true;
                }

                OnPropertyChanged(nameof(NewMedicineSelection));
            }
        }
        public string SelectedID { get; set; }
        public string SelectedName { get; set; }
        public string SelectedState { get; set; }
        public int SelectedStateIndex { get; set; }
        public List<IngredientListItemViewModel> SelectedIngredients { get; set; }


        private readonly ObservableCollection<MedicineListItemViewModel> _medicine;
        public IEnumerable<MedicineListItemViewModel> Medicine => _medicine;

        private readonly ObservableCollection<IngredientListItemViewModel> _ingredients;
        public IEnumerable<IngredientListItemViewModel> Ingredients => _ingredients;

        private string _newMedicineName;
        public string NewMedicineName
        {
            get => _newMedicineName;
            set
            {
                _newMedicineName = value;
                OnPropertyChanged(nameof(NewMedicineName));
            }
        }

        private string _selectedMedicineDescription;
        public string SelectedMedicineDescription
        {
            get => _selectedMedicineDescription;
            set
            {
                _selectedMedicineDescription = value;
                OnPropertyChanged(nameof(SelectedMedicineDescription));
            }
        }

        public AdminNavigationViewModel Navigation { get; }

        public ICommand CreateNewMedication { get; set; }
        public ICommand CreateNewIngredient { get; set; }
        public ICommand Details { get; set; }
        public ICommand ChangeMedicine { get; set; }
        public ICommand DeleteIngredient { get; set; }

        public AdminMedicineViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            _medicine = new ObservableCollection<MedicineListItemViewModel>();
            _ingredients = new ObservableCollection<IngredientListItemViewModel>();
            Navigation = new AdminNavigationViewModel();

            CreateNewMedication = new CreateMedicineCommand(this);

            FillMedicineList();
            FillIngredientList();
        }

        public void FillIngredientList()
        {
            _ingredients.Clear();

            foreach (Allergen a in Institution.Instance().AllergenRepository.Allergens)
            {
                _ingredients.Add(new IngredientListItemViewModel(a));
            }
        }

        public void FillMedicineList()
        {
            _medicine.Clear();

            foreach (PendingMedicine m in Institution.Instance().PendingMedicineRepository.PendingMedicines)
            {
                _medicine.Add(new MedicineListItemViewModel(m));
            }

        }
    }
}
