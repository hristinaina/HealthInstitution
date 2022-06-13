using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Core.Services;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class UpdateMedicalRecordCommand : BaseCommand
    {
        private UpdateMedicalRecordViewModel _viewModel;

        public UpdateMedicalRecordCommand(UpdateMedicalRecordViewModel medicalRecordViewModel)
        {
            _viewModel = medicalRecordViewModel;
        }

        public override void Execute(object parameter)
        {
            Examination examination = _viewModel.Examination;
            Patient patient = examination.Patient;
            patient.Record.Weight = _viewModel.Weight;
            patient.Record.Height = _viewModel.Height;
            examination.Patient = patient;
            DoctorRescheduleAppointmentService doctorRescheduleAppointmentService = new();
            doctorRescheduleAppointmentService.RescheduleExamination(examination, examination.Date);
        }
    }
}
