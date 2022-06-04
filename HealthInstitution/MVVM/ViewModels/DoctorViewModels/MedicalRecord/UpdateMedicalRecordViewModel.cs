using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Enumerations;


namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class UpdateMedicalRecordViewModel : BaseViewModel
    {
        public DoctorNavigationViewModel Navigation { get; }

        private Institution _institution;
        public ICommand SaveCommand { get; }
        public ICommand SaveAllergenCommand { get; }
        public ICommand SaveIllnessCommand { get; }
        public ICommand SaveAnamnesisCommand { get; }
        public ICommand CreateReferralCommand { get; }
        public ICommand CreateReferralSpecCommand { get; }
        public ICommand CreatePrescriptionCommand { get; }
        public ICommand UpdateEquipmentQuantityCommand { get; }

        private ObservableCollection<AllergenItemViewModel> _allergens;
        public IEnumerable<AllergenItemViewModel> Allergens => _allergens;
        private ObservableCollection<Allergen> _newAllergens;
        public IEnumerable<Allergen> NewAllergens => _newAllergens;
        private ObservableCollection<IllnessItemViewModel> _illnesses;
        public IEnumerable<IllnessItemViewModel> Illnesses => _illnesses;
        private ObservableCollection<EquipmentItemViewModel> _equipments;
        public IEnumerable<EquipmentItemViewModel> Equipments => _equipments;
        public EquipmentItemViewModel SelectedEquipment { get; set; }
        private Examination _examination;
        public Examination Examination { get => _examination; }
        
        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors => _doctors;
        public Doctor SelectedDoctor { get; set; }
        private ObservableCollection<Specialization> _specializations;
        public ObservableCollection<Specialization> Specializations => _specializations;
        public Specialization SelectedSpecialization { get; set; }
        private ObservableCollection<TherapyMealDependency> _therapyMealDependencies;
        public ObservableCollection<TherapyMealDependency> TherapyMealDependencies => _therapyMealDependencies;
        public TherapyMealDependency SelectedDependency { get; set; }
        private ObservableCollection<Medicine> _medicines;
        public ObservableCollection<Medicine> Medicines => _medicines;
        public Medicine SelectedMedicine { get; set; }
        public int DailyFrequency { get; set; }
        public int LongitudeInDays { get; set; }
        private Patient _patient;
        public Patient Patient { get => _patient; }
        public Allergen NewAllergen { get; set; }
        public string NewIllness { get; set; }

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

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private double _height;
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private double _weight;
        public double Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        private string _anamnesis;
        public string Anamnesis
        {
            get
            {
                return _anamnesis;
            }
            set
            {
                _anamnesis = value;
                OnPropertyChanged(nameof(Anamnesis));
            }
        }

        private int _spentEquipment;
        public int SpentEquipment
        {
            get
            {
                return _spentEquipment;
            }
            set
            {
                _spentEquipment = value;
                OnPropertyChanged(nameof(SpentEquipment));
            }
        }

        private bool _enableChanges;
        public bool EnableChanges
        {
            get => _enableChanges;
            set
            {
                _enableChanges = value;
                OnPropertyChanged(nameof(EnableChanges));
            }
        }

        private int _selection;
        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; };
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }

        public UpdateMedicalRecordViewModel(Examination examination)
        {
            bool isSpecialist = true;
            Doctor doctor = (Doctor)Institution.Instance().CurrentUser;
            _patient = examination.Patient;
            if (doctor.Specialization == Specialization.NONE) isSpecialist = false;
            Navigation = new DoctorNavigationViewModel(isSpecialist);

            _institution = Institution.Instance();
               
            _examination = examination;
            _allergens = new ObservableCollection<AllergenItemViewModel>();
            _newAllergens = new ObservableCollection<Allergen>();
            _illnesses = new ObservableCollection<IllnessItemViewModel>();
            _doctors = new ObservableCollection<Doctor>();
            _specializations = new ObservableCollection<Specialization>();
            _therapyMealDependencies = new ObservableCollection<TherapyMealDependency>();
            _medicines = new ObservableCollection<Medicine>();
            _equipments = new ObservableCollection<EquipmentItemViewModel>();

            SaveCommand = new UpdateMedicalRecordCommand(this);
            SaveAllergenCommand = new SaveAllergenCommand(this);
            SaveIllnessCommand = new SaveIllnessCommand(this);
            SaveAnamnesisCommand = new SaveAnamnesisCommand(this);
            CreateReferralCommand = new CreateReferralCommand(this);
            CreateReferralSpecCommand = new CreateReferralSpecCommand(this);
            CreatePrescriptionCommand = new CreatePrescriptionCommand(this);
            UpdateEquipmentQuantityCommand = new UpdateEquipmentQuantityCommand(this);

            SetProperties();
            FillAllergensList();
            FillNewAllergensList();
            FillIllnessList();
            FillDoctorsList();
            FillSpecializationsList();
            FillThreapyMealDependenciesList();
            FillMedicinesList();
            FillEquipments();
        }

        public void SetProperties()
        {
            Name = Examination.Patient.FirstName + " " + Examination.Patient.LastName;
            Height = Examination.Patient.Record.Height;
            Weight = Examination.Patient.Record.Weight;
            Anamnesis = Examination.Anamnesis;
            SpentEquipment = 0;
        }

        public void FillAllergensList()
        {
            _allergens.Clear();
            foreach (Allergen allergen in _examination.Patient.Record.Allergens)
            {
                _allergens.Add(new AllergenItemViewModel(allergen));
            }
            OnPropertyChanged(nameof(Allergens));
        }

        public void FillNewAllergensList()
        {
            _newAllergens.Clear();

            foreach(Allergen allergen in Institution.Instance().AllergenRepository.Allergens)
            {
                foreach (Allergen i in _examination.Patient.Record.Allergens)
                {
                    if (allergen != null && i != null)
                    {
                        if (i.ID == allergen.ID) continue;
                    }
                }

                _newAllergens.Add(allergen);
            }
        }

        public void FillIllnessList()
        {
            _illnesses.Clear();
            foreach (string illness in _examination.Patient.Record.HistoryOfIllnesses) {
                _illnesses.Add(new IllnessItemViewModel(illness));
            }
        }

        public void FillDoctorsList()
        {
            _doctors.Clear();
            foreach (Doctor doctor in _institution.DoctorRepository.Doctors)
            {
                _doctors.Add(doctor);
            }
            OnPropertyChanged(nameof(Doctors));
        }

        public void FillSpecializationsList()
        {
            _specializations.Clear();
            foreach (Specialization specialization in Enum.GetValues(typeof(Specialization)))
            {
                _specializations.Add(specialization);
            }
        }

        public void FillThreapyMealDependenciesList()
        {
            _therapyMealDependencies.Clear();
            foreach(TherapyMealDependency dependency in Enum.GetValues(typeof(TherapyMealDependency)))
            {
                _therapyMealDependencies.Add(dependency);
            }
        }

        public void FillMedicinesList()
        {
            _medicines.Clear();
            foreach(Medicine medicine in _institution.MedicineRepository.Medicines)
            {
                _medicines.Add(medicine);
            }
        }

        public void AddAllergen(Allergen allergen)
        {
            foreach(AllergenItemViewModel allergenViewModel in _allergens)
            {
                if (allergenViewModel.AllergenName == allergen.Name) return;
            }
            _allergens.Add(new AllergenItemViewModel(allergen));
        }

        public void AddIllness(string illness)
        {
            _illnesses.Add(new IllnessItemViewModel(illness));
        }

        public void FillEquipments()
        {
            _equipments.Clear();
            foreach (Equipment equipment in _examination.Room.Equipment.Keys)
            {
                Equipment equipmentInRoom = new Equipment(equipment);  // copying equipment
                equipmentInRoom.Quantity = equipment.ArrangmentByRooms[_examination.Room];
                _equipments.Add(new EquipmentItemViewModel(equipmentInRoom));
            }
            SetSelectedEquipment();
        }

        public void UpdateQuantity(Equipment equipment, int quantity)
        {
            foreach (Equipment i in _examination.Room.Equipment.Keys)
            {
                if (i.ID == equipment.ID) i.ArrangmentByRooms[_examination.Room] = quantity;
            }
            FillEquipments();
        }

        public void SetSelectedEquipment()
        {
            if (_equipments.Count > 0) SelectedEquipment = _equipments[0];
        }

        public int GetPreviousQuantityOfSelectedEquipment()
        {
            foreach (Equipment i in _examination.Room.Equipment.Keys)
            {
                if (i.ID == SelectedEquipment.Equipment.ID) return i.ArrangmentByRooms[_examination.Room];
            }

            return 0;
        }

    }
}
