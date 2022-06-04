using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using System;
using System.Collections.Generic;

namespace HealthInstitution.MVVM.Models.Services
{
    public static class AppointmentService
    {
        public static List<Appointment> SearchByAnamnesis(Patient patient, string keyWord)
        {
            List<Appointment> searchResults = new List<Appointment>();
            foreach (Appointment appointment in patient.GetPastAppointments())
            {
                if (appointment is Examination examination)
                {
                    if (examination.Anamnesis.ToLower().Contains(keyWord.ToLower()))
                    {
                        searchResults.Add(appointment);
                    }
                }
            }
            return searchResults;
        }

        public static List<Examination> MakeSuggestions(Patient patient, SchedulingPriority priority, Doctor doctor, DateTime deadlineDate, DateTime startTime, DateTime endTime)
        {
            List<Examination> suggestions = new List<Examination>();
            DateTime startDateTime = DateTime.Now;
            DateTime endDateTime = DateTime.Now;
            startDateTime += ShiftDateTime(startTime, startDateTime);
            endDateTime += ShiftDateTime(endTime, endDateTime);

            while (startDateTime < deadlineDate)
            {
                if (startDateTime >= endDateTime)
                {
                    startDateTime += ShiftDateTime(startTime, startDateTime);
                    endDateTime += ShiftDateTime(endTime, endDateTime);
                }
                while ((!patient.IsAvailable(startDateTime) || !doctor.IsAvailable(startDateTime)) && startDateTime < endDateTime)
                {
                    startDateTime = CheckInterruption(doctor, startDateTime);
                    startDateTime = CheckInterruption(patient, startDateTime);
                }

                if (startDateTime < endDateTime && patient.IsAvailable(startDateTime) && doctor.IsAvailable(startDateTime))
                {
                    suggestions.Add(new Examination(0, doctor, patient, startDateTime, null));
                    break;
                }
            }
            if (suggestions.Count == 0)
            {
                if (priority == SchedulingPriority.DOCTOR)
                {
                    suggestions = MakeAlternativeSuggestions(patient, doctor);
                }
                else
                {
                    startDateTime = DateTime.Now;
                    endDateTime = DateTime.Now;
                    startDateTime += 2 * ShiftDateTime(startTime, startDateTime);
                    endDateTime += 2 * ShiftDateTime(endTime, endDateTime);
                    suggestions = MakeAlternativeSuggestions(patient, startDateTime, endDateTime, deadlineDate);
                }
            }
            return suggestions;
        }

        private static List<Examination> MakeAlternativeSuggestions(Patient patient, Doctor doctor)
        {
            List<Examination> suggestions = new List<Examination>();
            DateTime startDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
            while (suggestions.Count != 3)
            {
                while (!patient.IsAvailable(startDateTime) || !doctor.IsAvailable(startDateTime))
                {
                    startDateTime = CheckInterruption(doctor, startDateTime);
                    startDateTime = CheckInterruption(patient, startDateTime);
                }
                suggestions.Add(new Examination(0, doctor, patient, startDateTime, null));
                startDateTime += new TimeSpan(0, 15, 0);
            }
            return suggestions;
        }


        private static List<Examination> MakeAlternativeSuggestions(Patient patient, DateTime startTime, DateTime endTime, DateTime deadlineDate)
        {
            List<Examination> suggestions = new List<Examination>();
            DateTime startDateTime = DateTime.Now;
            DateTime endDateTime = DateTime.Now;
            startDateTime += ShiftDateTime(startTime, startDateTime);
            endDateTime += ShiftDateTime(endTime, endDateTime);

            while (suggestions.Count < 3 && startDateTime < deadlineDate)
            {
                if (startDateTime >= endDateTime)
                {
                    startDateTime += ShiftDateTime(startTime, startDateTime);
                    endDateTime += ShiftDateTime(endTime, endDateTime);
                }
                while (!patient.IsAvailable(startDateTime) && startDateTime < endDateTime)
                {
                    startDateTime = CheckInterruption(patient, startDateTime);
                }
                AssignDoctor(patient, startTime, suggestions, startDateTime);
                startDateTime += new TimeSpan(0, 15, 0);
            }
            return suggestions;
        }


        private static void AssignDoctor(Patient patient, DateTime startTime, List<Examination> suggestions, DateTime startDateTime)
        {
            foreach (Doctor doctor in Institution.Instance().DoctorRepository.GetGeneralPractitioners())
            {
                if (doctor.IsAvailable(startTime))
                {
                    suggestions.Add(new Examination(0, doctor, patient, startDateTime, null));
                }
            }
        }

        private static DateTime FixTimeInterruption(Appointment interrupting)
        {
            DateTime startDateTime = interrupting.Date;
            int duration = 15;
            if (interrupting is Operation interruptingOperation)
            {
                duration = interruptingOperation.Duration;
            }
            startDateTime += new TimeSpan(0, duration + 1, 0);
            return startDateTime;
        }


        private static DateTime CheckInterruption(User user, DateTime startDateTime)
        {
            if (!user.isAvailable(startDateTime))
            {
                Appointment interrupting = user.FindInterruptingAppointment(startDateTime);
                startDateTime = FixTimeInterruption(interrupting);
            }

            return startDateTime;
        }

        private static TimeSpan ShiftDateTime(DateTime startTime, DateTime startDateTime)
        {
            return new TimeSpan(1, startTime.Hour - startDateTime.Hour, startTime.Minute - startDateTime.Minute, 0);
        }

        public static void DeleteFutureAppointments(Patient patient)
        {
            List<Examination> examinations = new List<Examination>(Institution.Instance().ExaminationRepository.Examinations.ToArray());
            List<Operation> operations = new List<Operation>(Institution.Instance().OperationRepository.Operations.ToArray());

            foreach (Examination appointment in examinations)
            {
                if (appointment.Date >= DateTime.Now && patient.ID == appointment.Patient.ID) DeleteAppointment(appointment);
            }
            foreach (Operation appointment in operations)
            {
                if (appointment.Date >= DateTime.Now && patient.ID == appointment.Patient.ID) DeleteAppointment(appointment);
            }

            Institution.Instance().ExaminationChangeRepository.DeleteUnresolvedRequestsByPatientId(patient.ID);
        }

        // secretary uses this function
        public static void DeleteAppointment(Appointment appointment)
        {
            Patient patient = appointment.Patient;
            Doctor doctor = appointment.Doctor;
            Room room = appointment.Room;

            Institution.Instance().ExaminationChangeRepository.RemoveByAppointmentId(appointment.ID);

            if (appointment is Examination)
            {
                patient.Examinations.Remove((Examination)appointment);
                doctor.Examinations.Remove((Examination)appointment);
                room.Appointments.Remove(appointment);
                Institution.Instance().ExaminationRepository.Remove((Examination)appointment);
                Institution.Instance().ExaminationReferencesRepository.Remove((Examination)appointment);
            }
            else if (appointment is Operation)
            {
                patient.Operations.Remove((Operation)appointment);
                doctor.Operations.Remove((Operation)appointment);
                Institution.Instance().OperationRepository.Remove((Operation)appointment);
                Institution.Instance().OperationReferencesRepository.Remove((Operation)appointment);
                room.Appointments.Remove(appointment);
            }
        }
    }
}

