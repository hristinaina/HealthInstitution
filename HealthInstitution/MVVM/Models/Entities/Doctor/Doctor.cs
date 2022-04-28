using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Doctor : User
    {
        private Specialization _specialization;
        private List<Examination> _examinations;
        private List<Operation> _operations;
        private List<DayOff> _daysOff;
        private double _rating;

        [JsonProperty("Specialization")]
        public Specialization Specialization { get => _specialization; set { _specialization = value; } }
        [JsonProperty("Rating")]
        public double Rating { get => _rating; set { _rating = value; } }
        [JsonIgnore]
        public List<Examination> Examinations { get => _examinations; set { _examinations = value; } }
        [JsonIgnore]
        public List<Operation> Operations { get => _operations; set { _operations = value; } }
        [JsonIgnore]
        public List<DayOff> DaysOff { get => _daysOff; set { _daysOff = value; } }

        public Doctor()
        {
            _daysOff = new List<DayOff>();
            _examinations = new List<Examination>();
            _operations = new List<Operation>();
            _daysOff = new List<DayOff>();
        }

        public Doctor(Specialization specialization = Specialization.NONE) : this()
        {
            _specialization = specialization;
        }

        public bool IsAvailable(DateTime dateTime, int durationInMin = 15)
        {

            Appointment interruptingAppointment = FindInterruptingAppointment(dateTime, durationInMin);
            if (interruptingAppointment is null) return true;
            return false;
        }

        private Appointment FindInterruptingAppointment(DateTime dateTime, int durationInMin = 15)
            // returns null if appointment can be reserved
            // else returns appointment that interrupts (scheduled appoint.) - for the next free appointment calculation
        {
            for (int i = 0; i < _examinations.Count(); i++)
            {
                DateTime examinationStart = _examinations[i].Date;
                DateTime examinationEnd = examinationStart.AddMinutes(15);
                if (DateTime.Compare(_examinations[i].Date.Date, dateTime.Date) != 0) continue;
                if (DateTime.Compare(dateTime, examinationStart) >= 0 &&
                    DateTime.Compare(dateTime, examinationEnd) < 0) return _examinations[i];  
                if (DateTime.Compare(dateTime.AddMinutes(durationInMin), examinationStart) > 0 &&
                    DateTime.Compare(dateTime.AddMinutes(durationInMin), examinationEnd) <= 0)
                    return _examinations[i];
            }

            for (int i = 0; i < _operations.Count(); i++)
            {
                DateTime operationStart = _operations[i].Date;
                DateTime operationEnd = operationStart.AddMinutes(_operations[i].Duration);
                if (DateTime.Compare(_operations[i].Date.Date, dateTime.Date) != 0) continue;
                if (DateTime.Compare(dateTime, operationStart) >= 0 &&
                    DateTime.Compare(dateTime, operationEnd) < 0) return _operations[i];
                if (DateTime.Compare(dateTime.AddMinutes(durationInMin), operationStart) > 0 &&
                    DateTime.Compare(dateTime.AddMinutes(durationInMin), operationEnd) <= 0)
                    return _operations[i];
            }

            return null; 
        }

        public List<DateTime> FindFreeTime(DateTime date, int durationInMin = 15) 
        {
            List<DateTime> availableTime = new();
            
            DateTime dateTime = date;
            dateTime = dateTime.AddHours(8);       // date == today, time == 8:00
            DateTime borderTime = date;
            borderTime = borderTime.AddHours(20);  // doctor works till 20 pm

            while (DateTime.Compare(dateTime.AddMinutes(durationInMin), borderTime) <= 0) 
            {
                Appointment interruptingAppointment = FindInterruptingAppointment(dateTime, durationInMin);
                if (interruptingAppointment is null)
                {
                    availableTime.Add(dateTime);
                    dateTime = dateTime.AddMinutes(durationInMin);
                    continue;
                }

                if (interruptingAppointment.GetType().Equals(typeof(Examination)))
                {
                    dateTime = interruptingAppointment.Date.AddMinutes(15);  // continuing at the end of 
                }                                                                     // interrupting appointment  
                else
                {
                    Operation scheduledOperation = (Operation) interruptingAppointment;
                    dateTime = scheduledOperation.Date.AddMinutes(scheduledOperation.Duration);
                }
            }

            return availableTime;
        }

        // schedule for certain day and 3 days after
        public List<Examination> GetScheduleOfExaminations(DateTime date)
        {
            List<Examination> examinations = new();

            foreach (Examination examination in _examinations)
            {
                if (examination.Date >= date && date.AddDays(3) >= examination.Date)
                    examinations.Add(examination);
            }

            return examinations;
        }

        public List<Operation> GetScheduleOfOperations(DateTime date)
        {
            List<Operation> operations = new();
            foreach (Operation operation in _operations)
            {
                if (operation.Date >= date && date.AddDays(3) >= operation.Date)
                    operations.Add(operation);
            }

            return operations;
        }
    }
}
