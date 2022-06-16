using System;
using System.Collections.Generic;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.Core.Services.DoctorServices;
using HealthInstitution.Core.UseCases.Validation;

namespace HealthInstitution.Services
{
    public class SuggestionsService : ISuggestAppointment, ISuggestAlternativeAppointment
    {
        public List<Examination> MakeSuggestions(ExaminationQuery query)
        {
            new PatientSuggestionValidationService().ValidateSuggestionData(query);
            List<Examination> suggestions = new List<Examination>();
            DateTime startDateTime = DateTime.Now;
            DateTime endDateTime = DateTime.Now;
            startDateTime += ShiftDateTime(query.StartTime, startDateTime);
            endDateTime += ShiftDateTime(query.EndTime, endDateTime);
            PatientService patientService = new PatientService(query.Patient);
            DoctorService doctorService = new DoctorService(query.Doctor);

            while (startDateTime < query.DeadlineDate)
            {
                if (startDateTime >= endDateTime)
                {
                    startDateTime += ShiftDateTime(query.StartTime, startDateTime);
                    endDateTime += ShiftDateTime(query.EndTime, endDateTime);
                }
                while ((!patientService.IsAvailable(startDateTime) || !doctorService.IsAvailable(startDateTime)) && startDateTime < endDateTime)
                {
                    startDateTime = CheckInterruption(query.Doctor, startDateTime);
                    startDateTime = CheckInterruption(query.Patient, startDateTime);
                }

                if (startDateTime < endDateTime && patientService.IsAvailable(startDateTime) && doctorService.IsAvailable(startDateTime))
                {
                    suggestions.Add(new Examination(0, query.Doctor, query.Patient, startDateTime, null));
                    break;
                }
            }
            if (suggestions.Count == 0)
            {
                if (query.Priority == SchedulingPriority.DOCTOR)
                {
                    suggestions = MakeAlternativeSuggestions(query.Patient, query.Doctor);
                }
                else
                {
                    startDateTime = DateTime.Now;
                    endDateTime = DateTime.Now;
                    startDateTime += 2 * ShiftDateTime(query.StartTime, startDateTime);
                    endDateTime += 2 * ShiftDateTime(query.EndTime, endDateTime);
                    suggestions = MakeAlternativeSuggestions(query.Patient, startDateTime, endDateTime, query.DeadlineDate);
                }
            }
            return suggestions;
        }

        public List<Examination> MakeAlternativeSuggestions(Patient patient, Core.Doctor doctor)
        {
            List<Examination> suggestions = new List<Examination>();
            DateTime startDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
            PatientService patientService = new PatientService(patient);
            DoctorService doctorService = new DoctorService(doctor);
            while (suggestions.Count != 3)
            {
                while (!patientService.IsAvailable(startDateTime) || !doctorService.IsAvailable(startDateTime))
                {
                    startDateTime = CheckInterruption(doctor, startDateTime);
                    startDateTime = CheckInterruption(patient, startDateTime);
                }
                suggestions.Add(new Examination(0, doctor, patient, startDateTime, null));
                startDateTime += new TimeSpan(0, 15, 0);
            }
            return suggestions;
        }


        public List<Examination> MakeAlternativeSuggestions(Patient patient, DateTime startTime, DateTime endTime, DateTime deadlineDate)
        {
            List<Examination> suggestions = new List<Examination>();
            DateTime startDateTime = DateTime.Now;
            DateTime endDateTime = DateTime.Now;
            startDateTime += ShiftDateTime(startTime, startDateTime);
            endDateTime += ShiftDateTime(endTime, endDateTime);
            PatientService patientService = new PatientService(patient);

            while (suggestions.Count < 3 && startDateTime < deadlineDate)
            {
                if (startDateTime >= endDateTime)
                {
                    startDateTime += ShiftDateTime(startTime, startDateTime);
                    endDateTime += ShiftDateTime(endTime, endDateTime);
                }
                while (!patientService.IsAvailable(startDateTime) && startDateTime < endDateTime)
                {
                    startDateTime = CheckInterruption(patient, startDateTime);
                }
                AssignDoctor(patient, startTime, suggestions, startDateTime);
                startDateTime += new TimeSpan(0, 15, 0);
            }
            return suggestions;
        }


        public void AssignDoctor(Patient patient, DateTime startTime, List<Examination> suggestions, DateTime startDateTime)
        {
            foreach (Core.Doctor doctor in Institution.Instance().DoctorRepository.GetGeneralPractitioners())
            {
                DoctorService doctorService = new DoctorService(doctor);
                if (doctorService.IsAvailable(startTime))
                {
                    suggestions.Add(new Examination(0, doctor, patient, startDateTime, null));
                }
            }
        }

        public DateTime FixTimeInterruption(Appointment interrupting)
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


        public DateTime CheckInterruption(User user, DateTime startDateTime)
        {
            IUserAvailability availability;
            if (user is Patient patient)
            {
                availability = new PatientService(patient);
            }
            else
            {
                Core.Doctor doctor = (Core.Doctor)user;
                availability = new DoctorService(doctor);
            }
            if (!availability.IsAvailable(startDateTime))
            {
                Appointment interrupting = availability.FindInterruptingAppointment(startDateTime);
                if (interrupting is not null) { 
                    startDateTime = FixTimeInterruption(interrupting);
                }
            }
            return startDateTime;
        }

        public TimeSpan ShiftDateTime(DateTime startTime, DateTime startDateTime)
        {
            return new TimeSpan(1, startTime.Hour - startDateTime.Hour, startTime.Minute - startDateTime.Minute, 0);
        }
    }
}

