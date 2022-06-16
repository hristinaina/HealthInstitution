using HealthInstitution.Core;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Services
{
    public interface ISuggestAppointment
    {
        public List<Examination> MakeSuggestions(ExaminationQuery query);
        public DateTime FixTimeInterruption(Appointment interrupting);
        public DateTime CheckInterruption(User user, DateTime startDateTime);
        public TimeSpan ShiftDateTime(DateTime startTime, DateTime startDateTime);

    }
}