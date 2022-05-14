using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands
{
    public class CreateReferralAppointmentCommand : BaseCommand
    {
        private readonly Institution _institution;
        private AppointmentsViewModel _viewModel;

        public CreateReferralAppointmentCommand(AppointmentsViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            Referral referral = Institution.Instance().ReferralRepository.FindByID(_viewModel.SelectedReferralId);
            DateTime datetime = _viewModel.MergeTime(_viewModel.NewAppointmentDate, _viewModel.NewAppointmentTime);
            Patient patient = Institution.Instance().PatientRepository.FindByID(referral.PatientId);

            try
            {
                bool done = ScheduleAppointment(referral, patient, datetime);
                if (!done)
                {
                    MessageBox.Show("The appointment is not available. Please choose another date or time!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (PatientBlockedException e)
            {
                _viewModel.ShowMessage(e.Message, logOut: true);
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            patient.Record.Referrals.Remove(referral);
            Institution.Instance().ReferralRepository.Referrals.Remove(referral);
            _viewModel.FillReferralsList();
        }

        public bool ScheduleAppointment(Referral referral, Patient patient, DateTime datetime)
        {
            if (referral.DoctorId != -1)
            {
                Doctor doctor = Institution.Instance().DoctorRepository.FindByID(referral.DoctorId);
                if (!ScheduleAppointmentByDoctor(doctor, patient, datetime))
                {
                    return false;
                }
            }
            else
            {
                if (!ScheduleAppointmentBySpecialization(referral.Specialization, patient, datetime))
                {
                    return false;
                }
            }
            return true;
        }

        public bool ScheduleAppointmentBySpecialization(Specialization specialization, Patient patient, DateTime datetime)
        {
            bool done = false;
            bool specializationException = false;
            foreach (Doctor doctor in Institution.Instance().DoctorRepository.Doctors)
            {
                if (doctor.Specialization == specialization)
                {
                    specializationException = true;
                    done = ScheduleAppointmentByDoctor(doctor, patient, datetime);
                    if (done)
                    {
                        break;
                    }
                }
            }
            if (!specializationException) throw new Exception("There are currently no doctors with selected specialization that work in this hospital.");
            return done;
        }

        public bool ScheduleAppointmentByDoctor(Doctor doctor, Patient patient, DateTime datetime)
        {
            bool done = Institution.Instance().CreateAppointment(doctor, patient, datetime, nameof(Examination));
            if (done)
            {
                MessageBox.Show("Appointment successfully scheduled !");
                return true;
            }
            return false;
        }
    }
}
