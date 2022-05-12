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
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands
{
    public class ScheduleCommand : BaseCommand
    {
        private readonly Institution _institution;
        private EmergencyAppointmentViewModel _viewModel;

        private readonly NavigationStore _navigationStore;

        public ScheduleCommand(EmergencyAppointmentViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            Appointment appointmentToPostpone = _viewModel.SelectedAppointment.Appointment;
            DateTime oldDate = appointmentToPostpone.Date;
            DateTime newDate = _viewModel.AppointmentsNewDate[appointmentToPostpone];
            //RemoveAppointment(appointmentToPostpone);
            appointmentToPostpone.Date = newDate;   // hoce li ovo promijeniti datum appointmenta svugdje?
            //Institution.Instance().CreateAppointment(appointmentToPostpone.Doctor, appointmentToPostpone.Patient,
                //newDate, nameof(appointmentToPostpone), GetDuration(appointmentToPostpone), false);

            Specialization specialization = _viewModel.SelectedSpecialization;
            Patient patient = _viewModel.SelectedPatient;
            int duration = _viewModel.SelectedDuration;
            string type = duration == 15 ? nameof(Examination) : nameof(Operation);
            Doctor doctor = appointmentToPostpone.Doctor.Specialization == specialization ?
                appointmentToPostpone.Doctor : Institution.Instance().DoctorRepository.FindDoctorBySpecialization(specialization);

            bool done = false;
            while (!done)
            {
                try
                {
                    done = Institution.Instance().CreateAppointment(doctor, patient, oldDate, type, duration, false);
                    if (done)
                    {
                        MessageBox.Show("Emergency appointment has been successfully created !");
                        _navigationStore.CurrentViewModel = new AppointmentsViewModel();
                        return;
                    }
                }
                catch (Exception e) { } // do nothing

                Appointment interruptingAppointment = doctor.FindInterruptingAppointment(oldDate, duration);
                if (interruptingAppointment is null) interruptingAppointment = patient.FindInterruptingAppointment(oldDate, duration);
                Dictionary<Appointment, DateTime> appointmentNewDate = new();
                int interDuration = GetDuration(interruptingAppointment);
                EmergencyAppointmentViewModel.FindNewAppointmentTime(interruptingAppointment, interDuration, appointmentNewDate);
                interruptingAppointment.Date = appointmentNewDate[interruptingAppointment];  // hoce li ovo promijeniti datum?
                //RemoveAppointment(interruptingAppointment);
                //Institution.Instance().CreateAppointment(interruptingAppointment.Doctor, interruptingAppointment.Patient,
                    //appointmentNewDate[interruptingAppointment], nameof(interruptingAppointment), GetDuration(interruptingAppointment), false);
            }
        }

        private void RemoveAppointment(Appointment appointment)
        {
            if (appointment is Examination)
            {
                Institution.Instance().ExaminationChangeRepository.RemoveByAppointmentId(appointment.ID);
                appointment.Patient.Examinations.Remove((Examination)appointment);
                appointment.Doctor.Examinations.Remove((Examination)appointment);
                Institution.Instance().ExaminationRepository.Remove((Examination)appointment);
                Institution.Instance().ExaminationReferencesRepository.Remove((Examination)appointment);
                Institution.Instance().ExaminationChangeRepository.Add((Examination)appointment, appointment.Date, true, AppointmentStatus.DELETED);
            }
            else if (appointment is Operation)
            {
                Institution.Instance().ExaminationChangeRepository.RemoveByAppointmentId(appointment.ID);
                appointment.Patient.Operations.Remove((Operation)appointment);
                appointment.Doctor.Operations.Remove((Operation)appointment);
                Institution.Instance().OperationRepository.Remove((Operation)appointment);
                Institution.Instance().OperationReferencesRepository.Remove((Operation)appointment);

            }
            appointment.Room.Appointments.Remove(appointment);
        }

        private int GetDuration(Appointment appointment)
        {
            int duration = 15;
            if (appointment.GetType() == typeof(Operation))
            {
                Operation o = (Operation)appointment;
                duration = o.Duration;
            }
            return duration;
        }
    }
}
