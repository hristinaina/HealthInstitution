using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            MoveDateTime(startTime, endTime, ref startDateTime, ref endDateTime);

            while (startDateTime < deadlineDate)
            {
                if (startDateTime >= endDateTime)
                {
                    MoveDateTime(startTime, endTime, ref startDateTime, ref endDateTime);
                }
                while ((!patient.IsAvailable(startDateTime) || !doctor.IsAvailable(startDateTime)) && startDateTime < endDateTime)
                {
                    Appointment interrupting;
                    if (!doctor.IsAvailable(startDateTime))
                    {
                        interrupting = doctor.FindInterruptingAppointment(startDateTime);
                    }
                    else
                    {
                        interrupting = patient.FindInterruptingAppointment(startDateTime);
                    }
                    startDateTime = FixTimeInterruption(interrupting);

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
                    MoveDateTime(startTime, endTime, ref startDateTime, ref endDateTime);

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
                    Appointment interrupting;
                    if (!doctor.IsAvailable(startDateTime))
                    {
                        interrupting = doctor.FindInterruptingAppointment(startDateTime);
                    }
                    else
                    {
                        interrupting = patient.FindInterruptingAppointment(startDateTime);
                    }
                    startDateTime = FixTimeInterruption(interrupting);
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
            MoveDateTime(startTime, endTime, ref startDateTime, ref endDateTime);

            while (suggestions.Count() < 3 && startDateTime < deadlineDate)
            {
                if (startDateTime >= endDateTime)
                {
                    MoveDateTime(startTime, endTime, ref startDateTime, ref endDateTime);
                }
                while (!patient.IsAvailable(startDateTime) && startDateTime < endDateTime)
                {
                    if (!patient.IsAvailable(startDateTime))
                    {
                        Appointment interrupting = patient.FindInterruptingAppointment(startDateTime);

                        startDateTime = FixTimeInterruption(interrupting);
                    }
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

        private static void MoveDateTime(DateTime startTime, DateTime endTime, ref DateTime startDateTime, ref DateTime endDateTime)
        {
            startDateTime += new TimeSpan(1, startTime.Hour - startDateTime.Hour, startTime.Minute - startDateTime.Minute, 0);
            endDateTime += new TimeSpan(1, endTime.Hour - endDateTime.Hour, endTime.Minute - endDateTime.Minute, 0);
        }

    }
}

