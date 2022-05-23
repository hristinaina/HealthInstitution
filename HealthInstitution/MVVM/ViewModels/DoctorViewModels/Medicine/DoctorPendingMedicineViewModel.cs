using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands;
using HealthInstitution.MVVM.Models;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DoctorPendingMedicineViewModel : BaseViewModel
    {
        public DoctorNavigationViewModel Navigation { get; }

        public DoctorPendingMedicineViewModel()
        {
            bool isSpecialist = true;
            Doctor doctor = (Doctor)Institution.Instance().CurrentUser;
            if (doctor.Specialization == Specialization.NONE) isSpecialist = false;
            Navigation = new DoctorNavigationViewModel(isSpecialist);
        }

    }
}
