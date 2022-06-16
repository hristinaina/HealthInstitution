using HealthInstitution.Core;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Services
{
    public interface ISuggestAppointment
    {
        public List<Examination> MakeSuggestions(ExaminationQuery query;
        protected DateTime FixTimeInterruption(Appointment interrupting);
        protected DateTime CheckInterruption(User user, DateTime startDateTime);
        protected DateTime FixTimeInterruption(Appointment interrupting);


    }
}