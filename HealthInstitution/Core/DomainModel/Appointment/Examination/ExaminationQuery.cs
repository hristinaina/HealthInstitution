using System;

namespace HealthInstitution.Core
{
    public class ExaminationQuery
    {
        private readonly Patient _patient;

        public Patient Patient
        {
            get { return _patient; }
        }

        private readonly Doctor _doctor;

        public Doctor Doctor
        {
            get { return _doctor; }
        }

        private readonly DateTime _startTime;

        public DateTime StartTime
        {
            get { return _startTime; }
        }

        private readonly DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
        }

        private readonly DateTime _deadlineDate;

        public DateTime DeadlineDate
        {
            get { return _deadlineDate; }
        }

        private readonly SchedulingPriority _priority;

        public SchedulingPriority Priority
        {
            get { return _priority; }
        }

        public ExaminationQuery(Patient patient, Doctor doctor, DateTime startTime, DateTime endTime, DateTime deadlineDate, SchedulingPriority priority)
        {
            _patient = patient;
            _doctor = doctor;
            _startTime = startTime;
            _endTime = endTime;
            _deadlineDate = deadlineDate;
            _priority = priority;
        }
    }
}
